import { AfterViewInit, Component, OnInit, ViewChild, ViewChildren, Inject } from '@angular/core';
import { ChatService } from '../chat.service';
import { NgForm } from '@angular/forms';
import { FusePerfectScrollbarDirective } from '../../../../../core/directives/fuse-perfect-scrollbar/fuse-perfect-scrollbar.directive';
import { HubConnection, TransportType } from '@aspnet/signalr-client';
import { useridintity } from '../../../../../core/services/ApiAddress';
import { hub } from '../hub';
import { DOCUMENT } from '@angular/common';
import { id } from '@swimlane/ngx-datatable/release/utils';
import { PushNotificationsService } from '../PushNotificationsService';
@Component({
    selector   : 'fuse-chat-view',
    templateUrl: './chat-view.component.html',
    styleUrls  : ['./chat-view.component.scss'],
 
})
export class FuseChatViewComponent implements OnInit, AfterViewInit
{
    user: any;
    chat: any;
    dialog: any;
    contact: any;
    replyInput: any;
    selectedChat: any;
    @ViewChild(FusePerfectScrollbarDirective) directiveScroll: FusePerfectScrollbarDirective;
    @ViewChildren('replyInput') replyInputField;
    @ViewChild('replyForm') replyForm: NgForm;


    // private _hubConnection: HubConnection;
    nick = '';
    message = '';
   
    messages: string[] = [];
    constructor(private chatService: ChatService,private _hub: hub,@Inject(DOCUMENT) private document: any)
     {
 

        this._hub._hubConnection.on('SeenMsg', (messagesocket: any) => {
           
            this.chatService.SeenMessage(messagesocket);
                     var b=document.getElementById("p"+messagesocket.msgid);
                     b.innerHTML="OO";
                     console.log(messagesocket +"   => "+b)
                            });

          this._hub._hubConnection.on('Send', (messagesocket: any) => {
            
            const message = {
                id:messagesocket.id,
                who    : messagesocket.users,
                message: messagesocket.message,
                time   : new Date().toISOString(),
                read:messagesocket.read,
                sessionchat:messagesocket.sessionchat,
                courentchatid:this.selectedChat.chatId
            };
 
        console.log("GetMsg",message)
         
        this.dialog= chatService.PushMsgToUserChat(message);
            // this.dialog.push(message);
            
        
            this.readyToReply();
       
       
   
          });


         
    }




    public sendMessage(mesg:any): void {
        
        this._hub._hubConnection
          .invoke('Send', mesg)
          .then(() => mesg = '')
          .catch(err => console.error(err));   
      }

    ngOnInit()
    { 
       
     
     
        this.user = this.chatService.user;
        this.chatService.onChatSelected
            .subscribe(chatData => {
                if ( chatData )
                {
                       
                    // http://localhost:49223/images/
                    this.selectedChat = chatData;
                    this.contact = chatData.contact;
                      
                    this.dialog = chatData.dialog;
                    this.readyToReply();
                }
            });
    }
sss(msg :any){
 
console.log(msg)
 
var el = document.getElementById("www");
el.remove();
}
    ngAfterViewInit()
    {
        this.replyInput = this.replyInputField.first.nativeElement;
        this.readyToReply();
    }

    selectContact()
    {      
        this.chatService.selectContact(this.contact);
    }

    readyToReply()
    {
        setTimeout(() => {
            this.replyForm.reset();
            this.focusReplyInput();
            this.scrollToBottom();
        });

    }

    focusReplyInput()
    {
        setTimeout(() => {
            this.replyInput.focus();
        });
    }

    scrollToBottom(speed?: number)
    {
        speed = speed || 400;
        if ( this.directiveScroll )
        {
            this.directiveScroll.update();

            setTimeout(() => {
                this.directiveScroll.scrollToBottom(0, speed);
            });
        }
    }

    reply(event)
    {
        
        // Message
        const message = {
            who    : this.user.id,
            message: this.replyForm.form.value.message,
            time   : new Date().toISOString()

        };
         
   const messagesend = {
            userr    : this.contact.id,
            users:this.user.id,
            message: this.replyForm.form.value.message,
            sessionchat:this.selectedChat.chatId
            
        };
        // Add the message to the chat
         
        // this.dialog.push(message);

        // Update the server
        this.readyToReply();
        this.sendMessage(messagesend);
        // this.chatService.updateDialog( messagesend).then(response => {
           
             
        // });

    }
}
