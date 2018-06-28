import { Component, OnDestroy, OnInit, AfterViewInit, ElementRef, Inject } from '@angular/core';
import { ChatService } from '../../../chat.service';
import { FormControl, FormGroup } from '@angular/forms';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';

@Component({
    selector   : 'fuse-chat-user-sidenav',
    templateUrl: './user.component.html',
    styleUrls  : ['./user.component.scss']
})
export class FuseChatUserSidenavComponent implements OnInit, OnDestroy, AfterViewInit
{
    elem:ElementRef;
    user: any;
    onFormChange: any;
    userForm: FormGroup;
      styleimg:string;
    constructor(public chatService: ChatService,  @Inject(ElementRef) elementRef: ElementRef)
    {
         
        this.elem = elementRef;
        this.user = this.chatService.user;
        this.userForm = new FormGroup({
            mood  : new FormControl(this.user.mood),
            status: new FormControl(this.user.status)
        });


        
         
    }

    ngOnInit()
    {   
        this.onFormChange = this.userForm.valueChanges
                                .debounceTime(500)
                                .distinctUntilChanged()
                                .subscribe(data => {
                                  
                                    this.user.mood = data.mood;
                                    this.user.status = data.status;
                                    this.chatService.updateUserData(this.user);
                                });
        if(this.user.avatar!=null)
                                $("#imagePreview").css(
                                    "background-image",
                                    "url(http://localhost:49223/images/"+this.user.avatar+")"
                                );
                                else
                                $("#imagePreview").css(
                                    "background-image",
                                    "url(https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png)"
                                );
           
    }
    ngAfterViewInit(): void {
    

      }
      importFile(event) {
        if (event.target.files && event.target.files[0]) {
          var reader = new FileReader();
        
          reader.onload = (event:any) => {
        ;
       $("#imagePreview").css(
        "background-image",
        "url(" + event.target.result + ")"
    );
    $("#imagePreview").hide();
    $("#imagePreview").fadeIn(650);
          }
        
          reader.readAsDataURL(event.target.files[0]);
        }
     
        let files=this.elem.nativeElement.querySelector('#imageUpload').files;
      let formData:FormData=new FormData();
    
      let file=files[0];
      formData.append('ProfilePhoto',file,file.name);         
      this.chatService.uploadSystemUserProfilePhoto("",formData).subscribe(res=>{
          
this.chatService.setavatar(res)

      })
        }
    changeLeftSidenavView(view)
    {  
        this.chatService.onLeftSidenavViewChanged.next(view);
    }

    ngOnDestroy()
    {  
        this.onFormChange.unsubscribe();
    }
    UploadImg(formdata:any){
         
        this.chatService.uploadSystemUserProfilePhoto("",formdata).subscribe(res=>{

    
        }) 

    }
}
