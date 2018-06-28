import {Pipe, PipeTransform} from "@angular/core";
import { hub } from "./hub";
import { ChatService } from "./chat.service";
import { useridintity } from "../../../../core/services/ApiAddress";
 
@Pipe({
    name: 'SeenPipe'
})
export class SeenPipe implements PipeTransform{
    contact:any;
    constructor(private _hub: hub,private chatService: ChatService){

        this.contact = chatService.contacts;

  
    }

    public sendMessage(mesg:any): void {
         
        this._hub._hubConnection
          .invoke('Seen', mesg)
          .then(() =>      this.chatService.readmsgunred(mesg.chatid,mesg.msgid))
          .catch(err => console.error(err));   
      }

    transform(value: any,msgid:number,who:number,chatid:number){
   
    
 
    
       
        if( who!=useridintity.userid){
            const seen={
                usernamecontact:this.contact.filter(x=> x.id==who).map(x=> x.username)[0],
                msgid:msgid,
                username:useridintity.username,
                 chatid:chatid
        
                 }
                 console.log("Seen"+seen);
            
            this.sendMessage(seen)
  
             value="OO"
        }
          else {
                value="O"
               
            }
         
        return value;
    }
}