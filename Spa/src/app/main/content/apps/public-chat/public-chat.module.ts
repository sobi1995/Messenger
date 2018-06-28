import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatRoomComponent } from './chat-room/chat-room.component';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
const appRoutes: Routes = [
  {
     path: 'Loginlayout',
      component: ChatRoomComponent,
          children:[
            {path:'ChatRoom',component: ChatRoomComponent}
          ]
}
 ];
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(appRoutes) ,
    FormsModule
  ],
  declarations: [ChatRoomComponent]
})
export class PublicChatModule { }
