import { Component, OnInit, ViewContainerRef, AfterViewInit, Inject, ElementRef } from '@angular/core';
import { DOCUMENT } from '@angular/platform-browser';
import { Form, FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FuseConfigService } from '../../../core/services/config.service';
import { MatDialog } from '@angular/material';
import { ToastsManager } from 'ng2-toastr';
import { IdentityService } from '../LoginService';
import { useridintity } from '../../../core/services/ApiAddress';
import { FuseContactsContactFormDialogComponent } from '../../../main/content/apps/chat/Modal/contact-form/contact-form.component';
import * as $ from 'jquery';
 

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit  {
  elem:ElementRef;
  ngOnInit(): void {
   
  }
 
  
title:string;
LoinFrmGrp:FormGroup;

  constructor(
     private fuseConfig: FuseConfigService,
    fb:FormBuilder,
    private _router: Router,
    private _IdentityService:IdentityService,
    public toastr: ToastsManager, 
    vcr: ViewContainerRef,
    public dialog: MatDialog,
    @Inject(ElementRef) elementRef: ElementRef,
    @Inject(DOCUMENT) private document: any
  ) { 
    this.elem = elementRef;
    this.fuseConfig.setSettings({
    
  });
  this.toastr.setRootViewContainerRef(vcr);
 
 
this.LoinFrmGrp=new   FormGroup({
  username:new  FormControl('', Validators.compose([Validators.required,Validators.pattern("^(\\+98|0)?9\\d{9}$")])),
  password:new  FormControl('',Validators.required),

})

 
 }

 Login(){
 if(!this.LoinFrmGrp.valid){
 this.showError("please check the validations")
 return
}
  this._IdentityService.login(this.LoinFrmGrp.controls.username.value,
                           this.LoinFrmGrp.controls.password.value)
        .subscribe(res => {
          console.log(res);
            localStorage.setItem('access_token', res.access_token);
            localStorage.setItem('refresh_token', res.refresh_token);
            useridintity.token=res.access_token;
         
            this._IdentityService.GetIdintity().subscribe(res=>{
              useridintity.username=res.username;
              useridintity.userid=res.userData;
               
              this._router.navigate(['/PanelLayot/ChatApp']);
            });
        }
        , error => this.showError('The UserName OR Password Is Wrong \n Please Try Again'));

  
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

   

 
 