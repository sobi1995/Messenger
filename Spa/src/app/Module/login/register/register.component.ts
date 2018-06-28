import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
 
import { UserRegisterDto } from './UserRegisterDto';
import { IdentityService } from '../LoginService';
import { MatDialog } from '@angular/material';
import { ToastsManager } from 'ng2-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerFrm:FormGroup;
  constructor(  fb:FormBuilder, private _IdentityService:IdentityService,
    vcr: ViewContainerRef,
    public toastr: ToastsManager, 
    private _router: Router) {
      this.toastr.setRootViewContainerRef(vcr);
    this.registerFrm=new   FormGroup({
      FirstName:  new  FormControl('',Validators.required),
      LastName:  new  FormControl('',Validators.required),
      Password: new  FormControl('',Validators.required),
      Mobile:  new  FormControl('', Validators.compose([Validators.required,Validators.pattern("^(\\+98|0)?9\\d{9}$")])),
     })

   }

  ngOnInit() {
  }
  Register(){
    if(!this.registerFrm.valid){
      this.showError("please check the validations")
      return
     }
 
this._IdentityService.RregisterUser(new UserRegisterDto(
  this.registerFrm.controls.FirstName.value,
  this.registerFrm.controls.Password.value,
  this.registerFrm.controls.Mobile.value,
  this.registerFrm.controls.LastName.value)).
  subscribe(res => {
     
    if(res==2) this.showWarning("The phone Number Exsit s");
    else if(res==1)
    {
      this.showSuccess("you are registered Please Login...");
      this._router.navigate(['/Loginlayout/Login']);
    }
console.log(res);
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