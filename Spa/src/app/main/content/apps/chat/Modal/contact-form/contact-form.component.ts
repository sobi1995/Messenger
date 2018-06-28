import { Component, Inject, OnInit, ViewEncapsulation, ViewContainerRef } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { CalendarEvent } from 'angular-calendar';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Contact } from './contact.model';
import { ChatService } from '../../chat.service';
import { useridintity } from '../../../../../../core/services/ApiAddress';
import { ToastsManager } from 'ng2-toastr';
 

@Component({
    selector     : 'fuse-contacts-contact-form-dialog',
    templateUrl  : './contact-form.component.html',
    styleUrls    : ['./contact-form.component.scss'],
    encapsulation: ViewEncapsulation.None
})

export class FuseContactsContactFormDialogComponent implements OnInit
{
    event: CalendarEvent;
    dialogTitle: string;
    contactForm: FormGroup;
    action: string;
    // contact: Contact;
   
    constructor(
        public dialogRef: MatDialogRef<FuseContactsContactFormDialogComponent>,
        @Inject(MAT_DIALOG_DATA) private data: any,
        private formBuilder: FormBuilder,
        private chatService: ChatService,
        public toastr: ToastsManager, 
        vcr: ViewContainerRef,
      
    )
    {
        this.toastr.setRootViewContainerRef(vcr);
        this.action = data.action;

        // if ( this.action === 'edit' )
        // {
        //     this.dialogTitle = 'Edit Contact';
        //     this.contact = data.contact;
        // }
        // else
        // {
        //     this.dialogTitle = 'New Contact';
        //     this.contact = new Contact({});
        // }
        // this.contact = new Contact({});
        this.contactForm = this.createContactForm();
    }

    ngOnInit()
    {
    }

    createContactForm()
    {
        return this.formBuilder.group({
            nickname: [],
            phone   : [],
            notes   : []
        });
    }
    CreateNewContact(){
        
  
          this.chatService.AddContact(new Contact(
            useridintity.userid,
            this.contactForm.controls.nickname.value,
            this.contactForm.controls.notes.value,
            this.contactForm.controls.phone.value
          ) ).then(res=>{
              if(res==2)this.showWarning("Phone not found");
              else if(res==3)this.showWarning("The already is your contact ");
              else if(res==0)this.showError("Server Error");
              else { this.showSuccess("The contact added");
            
            this.chatService.PushContact(res).then(res=>{});
            }
          })

    }


    showSuccess(message:string) {
        this.toastr.success(message, 'Success!');
      }
      
      showError(message:string) {
       
        this.toastr.error(message, 'Oops!');
      }
      
      showWarning(message:string) {
        this.toastr.warning(message, 'Alert!');
      }
      
      showInfo(message:string) {
        this.toastr.info(message);
      }
      
      showCustom(message:string) {
        this.toastr.custom('<span style="color: red">Message in red.</span>', null, {enableHTML: true});
      }


}
