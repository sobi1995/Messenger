import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
 
import { RouterModule, Routes } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
 
 
import { HttpModule } from '@angular/http';
import { LoginlayoutComponent } from '../../core/components/layout/loginlayout/loginlayout.component';
import { MaterialModule } from '../../core/modules/material.module';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { IdentityService } from './LoginService';


const routes: Routes = [
  // {
  //   path: 'Login',
  //   component: LoginComponent,
  //   data: {
  //     title: 'Login'
  //   }
  // }

  { 
    path: 'Loginlayout', 
    component: LoginlayoutComponent,
    children: [
      { path: 'Login', component: LoginComponent, pathMatch: 'full'},
      { path: 'register', component: RegisterComponent, pathMatch: 'full'},
    ]
},
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    RouterModule.forRoot(routes),
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    HttpModule
  ],
  declarations: [LoginComponent, RegisterComponent],
  providers:[IdentityService]
})
export class LoginModule { }
