import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { HubConnection, TransportType } from '@aspnet/signalr-client';
@Component({
  selector: 'app-chat-room',
  templateUrl: './chat-room.component.html',
  styleUrls: ['./chat-room.component.scss']
})
export class ChatRoomComponent implements OnInit {

  private _hubConnection: HubConnection;
  nick = '';
  message = '';
  messages: string[] = [];
  @ViewChild('YourselfChat') YourselfChat:ElementRef;
constructor(){

  this.nick = window.prompt('Your name:', 'John');
  const options = {
    transport: TransportType.WebSockets
};
  this._hubConnection = new HubConnection('http://localhost:49223/chat/?token='+localStorage.getItem("access_token"),options);


  this._hubConnection
    .start()
    .then(() => console.log('Connection started!'))
    .catch(err => console.log('Error while establishing connection :('+err));

    this._hubConnection.on('Send', (nick: string, receivedMessage: string) => {
      const text = `${nick}: ${receivedMessage}`;
      this.messages.push(text);
      console.log(text)
    });
}
  public sendMessage(): void {
    this._hubConnection
      .invoke('sendToAll', this.nick, this.message)
      .then(() => this.message = '')
      .catch(err => console.error(err));

      this.YourselfChat.nativeElement.append ="<br>something";
       
  }

  ngOnInit() {
  }

}
