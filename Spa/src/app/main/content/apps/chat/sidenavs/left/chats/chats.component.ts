import { Component, OnInit } from '@angular/core';
import { ChatService } from '../../../chat.service';
import { ObservableMedia } from '@angular/flex-layout';
import { fuseAnimations } from '../../../../../../../core/animations';
import { FuseMatSidenavHelperService } from '../../../../../../../core/directives/fuse-mat-sidenav-helper/fuse-mat-sidenav-helper.service';
import { hub } from '../../../hub';
import { IdentityService } from '../../../../../../../Module/login/LoginService';
import { useridintity } from '../../../../../../../core/services/ApiAddress';
import { ContectChatEntity } from './ContectChatEntity';

@Component({
    selector   : 'fuse-chat-chats-sidenav',
    templateUrl: './chats.component.html',
    styleUrls  : ['./chats.component.scss'],
    animations : fuseAnimations,
 
})
export class FuseChatChatsSidenavComponent implements OnInit
{
    user: any;
    chats: any;
    contacts: any;
    chatSearch: any;
    searchText = '';
 chatcontact:any[]=[];
    constructor(
        private chatService: ChatService,
        private fuseMatSidenavService: FuseMatSidenavHelperService,
        public media: ObservableMedia,
        private _hub: hub
    )
    {
     
        this.chatSearch = {
            name: ''
        };

 
        this._hub._hubConnection.on('SignalStatus', (status: any) => {       
       this.chatService.changeuserstatus(status.userid,status.status);
          });
     

          this._hub._hubConnection.on('AddContactSignal', (contact: any) => {
            
 this.chatService.AddContactPush(contact);
 this.ngOnInit();
          
        });
    }

    public sendMessage(usernamecontact:any,my:number,status:number): void {
         
        this._hub._hubConnection
          .invoke('StatusUser', usernamecontact,my,status)
          .then(() => {

          } )
          .catch(err => console.error(err));   
      }

    ngOnInit()
    {    
          
        this.user = this.chatService.user;
        this.chats = this.chatService.chats;
        this.contacts = this.chatService.contacts;
 
      this.chatcontact=[];
     this.contacts.forEach(element => {
         
         let itm={

            contact:element,
            chat:this.chats.filter(x=> x.contact==element.id)[0]
         }
         this.chatcontact.push(itm);
  
     });
     
      var a=this.chatcontact;
 
//        this.chatService.getUser().then(res=>{
//         debugger
//         this.user=res;
//         })
//         this.chatService.getChats().then(res=>{
//             debugger
//             this.chats=res;
//         }) 
//    this.chatService.getContacts().then(res=>{
//     debugger
//     this.contacts=res;
//         })
        this.chatService.onChatsUpdated.subscribe(updatedChats => {
            
            this.chats = updatedChats;
        });

        this.chatService.onUserUpdated.subscribe(updatedUser => {
             
            this.user = updatedUser;
        });
        
    }

    getChat(chatid,contact)
    {   
          
        this.chatService.getChat(chatid,contact);

        if ( !this.media.isActive('gt-md') )
        {  
            this.fuseMatSidenavService.getSidenav('chat-left-sidenav').toggle();
        }
    }

    setUserStatus(status)
    {
          
        var onlinecontact=this.contacts.map(x=> x.username);
        if(status=="online")
        this.sendMessage(onlinecontact,useridintity.userid,1)
     else   if(status=="offline")
        this.sendMessage(onlinecontact,useridintity.userid,0)
        this.chatService.setUserStatus(status);
       
    }
    

    changeLeftSidenavView(view)
    {
      
        this.chatService.onLeftSidenavViewChanged.next(view);
    }

    logout()
    {
        console.log('logout triggered');
    }
}
