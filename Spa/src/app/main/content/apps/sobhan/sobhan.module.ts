import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ComTestComponent } from './com-test/com-test.component';
import { Routes, RouterModule } from '@angular/router';
const appRoutes: Routes = [
  { path: 'xxxx', component: ComTestComponent }
 ];
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(appRoutes),
  ],
  declarations: [ComTestComponent]
})
export class SobhanModule { }
