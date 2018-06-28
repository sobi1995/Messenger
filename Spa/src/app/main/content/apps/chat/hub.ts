import { Injectable } from "@angular/core";
import { HubConnection, TransportType } from '@aspnet/signalr-client';
import { useridintity } from "../../../../core/services/ApiAddress";
import { ChatService } from "./chat.service";
import { PushNotificationsService } from "./PushNotificationsService";
import { Observable } from "rxjs/Observable";
@Injectable()
export class hub {
    public _hubConnection: HubConnection;
 
constructor(  private chatService: ChatService,public _notificationService: PushNotificationsService ){
    this._notificationService.requestPermission();
    const options = {
        transport: TransportType.WebSockets
    };
 

    
        this._hubConnection = new HubConnection('http://localhost:49223/chat/?token='+useridintity.token,options);


        this._hubConnection
          .start()
          .then(() => console.log('Connection started!'))
          .catch(err => console.log('Error while establishing connection :('+err));

       

          this._hubConnection.on('Send', (messagesocket: any) => {
             
            
            const message = {
                id:messagesocket.id,
                who    : messagesocket.users,
                message: messagesocket.message,
                time   : new Date().toISOString(),
                read:messagesocket.read,
                sessionchat:messagesocket.sessionchat,
         
            };
            this.chatService.PushMsgToUserChatWheneNotSelectContact(message);
            if(useridintity.userid!=message.who){
            let data: Array < any >= [];
            data.push({
                'title': 'Baran Message',
                'alertContent': messagesocket.message,
                'img':this.chatService.getContactByid(message.who).avatar
            });
      
            this._notificationService.generateNotification(data);
        }
            // this.showInfo(messagesocket.message);
            
          });




      
        
          this._hubConnection.onclose(() => { 
              console.log(":(Diconect");
            setTimeout(function(){
            this._hubConnection.start()
                       .done(() => {
                            this.startingSubject.next();
                            console.log(":)Connect");
                                //Invoke connect method on Hub
                              //  this.hubProxy.invoke("Connect", userId, usertype);
        
                           })
                          .fail((error: any) => {
                                this.startingSubject.error(error);
                           });
               },3000); 
           });
}
}
