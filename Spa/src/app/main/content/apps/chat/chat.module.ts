import { NgModule } from '@angular/core';
import { SharedModule } from '../../../../core/modules/shared.module';
import { RouterModule, Routes } from '@angular/router';
import { FuseChatComponent } from './chat.component';
import { ChatService } from './chat.service';
import { FuseChatViewComponent } from './chat-view/chat-view.component';
import { FuseChatStartComponent } from './chat-start/chat-start.component';
import { FuseChatChatsSidenavComponent } from './sidenavs/left/chats/chats.component';
import { FuseChatUserSidenavComponent } from './sidenavs/left/user/user.component';
import { FuseChatLeftSidenavComponent } from './sidenavs/left/left.component';
import { FuseChatRightSidenavComponent } from './sidenavs/right/right.component';
import { FuseChatContactSidenavComponent } from './sidenavs/right/contact/contact.component';
 
import { FuseContactsContactFormDialogComponent } from './Modal/contact-form/contact-form.component';
import { hub } from './hub';
import { SeenPipe } from './SeenPipe';
import { FuseMainComponent } from '../../../../core/components/layout/user-panellayout/main.component';
import { ChatContactPipe } from './ChatContactPipe';
import { ToastModule } from 'ng2-toastr';
import { PushNotificationsService } from './PushNotificationsService';

 
 

const routes: Routes = [
    {
        path     : 'PanelLayot',
        component: FuseMainComponent,
        children : [ { path: 'ChatApp', component: FuseChatComponent, pathMatch: 'full'},],
        resolve  : {
            chat: ChatService
        }
    }
];

@NgModule({
    imports     : [
        SharedModule,
        RouterModule.forChild(routes),
        ToastModule.forRoot()
    ],
    declarations: [
        FuseChatComponent,
        FuseChatViewComponent,
        FuseChatStartComponent,
        FuseChatChatsSidenavComponent,
        FuseChatUserSidenavComponent,
        FuseChatLeftSidenavComponent,
        FuseChatRightSidenavComponent,
        FuseChatContactSidenavComponent,
        FuseContactsContactFormDialogComponent,
        SeenPipe,
        ChatContactPipe
    ],
    providers   : [
        ChatService,
        hub,
        PushNotificationsService
    ],
    entryComponents:[FuseContactsContactFormDialogComponent]
})
export class FuseChatModule
{
}
