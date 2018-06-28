import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Subject } from 'rxjs/Subject';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { FuseUtils } from '../../../../core/fuseUtils';
import { useridintity } from '../../../../core/services/ApiAddress';
import { Contact } from './Modal/contact-form/contact.model';
import { RequestOptions } from '@angular/http';
import { hub } from './hub';

@Injectable()
export class ChatService implements Resolve<any>
{
    contacts: any[];
    chats: any[];
    user: any;
    onChatSelected = new BehaviorSubject<any>(null);
    onContactSelected = new BehaviorSubject<any>(null);
    onChatsUpdated = new Subject<any>();
    onUserUpdated = new Subject<any>();
    onLeftSidenavViewChanged = new Subject<any>();
    onRightSidenavViewChanged = new Subject<any>();

    constructor(private http: HttpClient)
    { //     this.user =ChatFakeDb.user[0]
        //     this.chats = ChatFakeDb.chats
        //     this.contacts = ChatFakeDb.contacts
     
    }

    /**
     * Get chat
     * @param contactId
     * @returns {Promise<any>}
     */
    getChat(chatid,contactId)
    { 
        var a=this.chats;
        var b=this.contacts;
        var c=this.user
        const chatItem = chatid

        /**
         * Create new chat, if it's not created yet.
         */
        // if ( !chatItem )
        // { 
        //     this.createNewChat(contactId).then((newChats) => {
        //         this.getChat(contactId);
                 
        //     });
        //     return;
        // }
         
    
      
        const chatData = {
            chatId : chatItem,
            dialog :this.chats.filter(x=> x.id==chatItem).map(x=> x.dialog)[0],
            contact: this.contacts.filter(x=> x.id==contactId)[0]
        };

        this.onChatSelected.next({...chatData});
      
//!!!باید از داخل خود انگولار گرفته شود
    //     return new Promise((resolve, reject) => {
    //         this.http.get('http://localhost:49223/api/Caht/chatContact?contactid='+contactId+'&my='+useridintity.userid)
    //              .subscribe((response: any) => {
    //                 const chat = response;
 
    //                 const chatContact = this.contacts.find((contact) => {
                    
    //                     return contact.id === contactId;
    //                 });
    //                 debugger
    // const chatDatsa = {
    //         chatId : chatItem.id,
    //         dialog :this.chats.filter(x=> x.id==chatItem.id).map(x=> x.dialog)[0],
    //         contact: this.contacts.filter(x=> x.id==contactId)[0]
    //     };
    //     debugger
    //                 const chatData = {
    //                     chatId : chat.id,
    //                     dialog : chat.dialog,
    //                     contact: chatContact
    //                 };
    //                 this.onChatSelected.next({...chatData});

    //             }, reject);

    //     });

    }

    /**
     * Create New Chat
     * @param contactId
     * @returns {Promise<any>}
     */
    createNewChat(contactId)
    { 
        return new Promise((resolve, reject) => {

            const contact = this.contacts.find((item) => {
                return item.id === contactId;
            });

            const chatId = FuseUtils.generateGUID();

            const chat = {
                id    : chatId,
                dialog: []
            };

            const chatListItem = {
                contactId      : contactId,
                id             : chatId,
                lastMessageTime: '2017-02-18T10:30:18.931Z',
                name           : contact.name,
                unread         : null
            };

            /**
             * Add new chat list item to the user's chat list
             */
            this.user.chatList.push(chatListItem);

            /**
             * Post the created chat
             */
            this.http.post('api/chat-chats', {...chat})
                .subscribe((response: any) => {

                    /**
                     * Post the new the user data
                     */
                    this.http.post('api/chat-user/' + this.user.id, this.user)
                        .subscribe(newUserData => {

                            /**
                             * Update the user data from server
                             */
                            this.getUser().then(updatedUser => {
                                this.onUserUpdated.next(updatedUser);
                                resolve(updatedUser);
                            });
                        });
                }, reject);
        });
    }

    /**
     * Select Contact
     * @param contact
     */
    selectContact(contact)
    {       
        this.onContactSelected.next(contact);
    }

    /**
     * Set user status
     * @param status
     */
    setUserStatus(status)
    {       
        this.user.status = status;
    }
changeuserstatus(userid :number,status :string){
 
    let index=this.contacts.findIndex(x=> x.id==userid);
    this.contacts[index].status=status;
     
}
    /**
     * Update user data
     * @param userData
     */
    updateUserData(userData)
    { 
        this.http.post('api/chat-user/' + this.user.id, userData)
            .subscribe((response: any) => {
               
                    this.user = userData;
                }
            );
    }

    /**
     * Update the chat dialog
     * @param chatId
     * @param dialog
     * @returns {Promise<any>}
     */
    updateDialog(message): Promise<any>
    { 
        var body=JSON.stringify(message);
        var headers = new HttpHeaders();
         headers.append('Content-Type', 'application/json');
       
         headers.append('Authorization', 'bearer ' + useridintity.token);
        return new Promise((resolve, reject) => {
            this.http.post('http://localhost:49223/api/Caht/SendMessage' , message,{headers:headers})       
                .subscribe(updatedChat => {               
                    resolve(updatedChat);
                }, reject);
        });
    }

    /**
     * The Chat App Main Resolver
     * @param {ActivatedRouteSnapshot} route
     * @param {RouterStateSnapshot} state
     * @returns {Observable<any> | Promise<any> | any}
     */
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any
    { 
        return new Promise((resolve, reject) => {
            
            Promise.all([
                
                this.getContacts(),
                this.getChats(),
                this.getUser()
            ]).then(
                ([contacts, chats, user]) => {
                     
                    this.contacts = contacts;
                    this.chats = chats;
                    this.user = user;
                    resolve();
                },
                reject
            );
        });
    }

    /**
     * Get Contacts
     * @returns {Promise<any>}
     */
    getContacts(): Promise<any>
    {  //'api/chat-contacts
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    headers.append('Authorization', 'bearer ' + useridintity.token);
        return new Promise((resolve, reject) => {
            this.http.get('http://localhost:49223/api/Contact?userid='+useridintity.userid,{headers:headers})
                .subscribe((response: any) => {
                     
                    resolve(response);
                }, reject);
        });
    }

    /**
     * Get Chats
     * @returns {Promise<any>}
     */
    getChats(): Promise<any>
    {          const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    headers.append('Authorization', 'bearer ' + useridintity.token);
        //api/chat-chats
        return new Promise((resolve, reject) => {
            this.http.get('http://localhost:49223/api/User/GetChatList?userid='+useridintity.userid,{headers:headers})
                .subscribe((response: any) => {
                     
                    resolve(response);
                }, reject);
        });
    }

    /**
     * Get User
     * @returns {Promise<any>}
     */
    getUser(): Promise<any>
    { 
        // api/chat-user
    
        const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        headers.append('Authorization', 'bearer ' + useridintity.token);
        return new Promise((resolve, reject) => {
            this.http.get('http://localhost:49223/api/User/getUser?userid='+useridintity.userid,{headers:headers})
                .subscribe((response: any) => {
                      
                    resolve(response);
                }, reject);
        });
    }

   
    AddContact(Contact:Contact): Promise<any>
    {
        
        const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        headers.append('Authorization', 'bearer ' + useridintity.token);
        const body = JSON.stringify(Contact); 
        return new Promise((resolve, reject) => {
            this.http.post('http://localhost:49223/api/Contact/AddContact',body,{headers:headers})
                .subscribe((response: any) => {
                    
                    resolve(response);
                }, reject);
        });
    }
    uploadSystemUserProfilePhoto(token:string,formData: FormData): Observable<any> {
      
             
       return this.http.post('http://localhost:49223/api/User/uploadsystemuserprofilephoto?userid='+useridintity.userid, formData)
               .map((response: Response) => {
                   
               });
}

PushContact(contactid:number)
{
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    headers.append('Authorization', 'bearer ' + useridintity.token);
   
    return new Promise((resolve, reject) => {
        this.http.get('http://localhost:49223/api/User/getUserContact?userid='+contactid,{headers:headers})
            .subscribe((response: any) => {
                 
            this.contacts.push(response);
                resolve(response);
            }, reject);
    });


}
SeenMessage(msg:any){
     
   let indexchat= this.chats.findIndex(x=> x.id==msg.chatid);
 
   this.chats[indexchat].dialog.filter(x=> x.id==msg.msgid)[0].read=1;
}
PushMsgToUserChat(msg:any){
    // const message = {
    //     id:msg.id,
    //     who    : msg.who,
    //     message: msg.message,
    //     time   : new Date().toISOString(),
    //     read:msg.read,    
    // };
//     if(this.user.id!=message.who){    
//     this.chats.filter(x=> x.id==msg.sessionchat )[0].unread=
//     this.chats.filter(x=> x.id==msg.sessionchat ).map(x=> x.dialog)[0].filter(x=> x.read==0).length
// }
//  this.chats.filter(x=> x.id==msg.sessionchat).map(x=> x.dialog)[0].push(message)
    if(msg.courentchatid==msg.sessionchat)
return this.chats.filter(x=> x.id==msg.sessionchat).map(x=> x.dialog)[0]
else
return this.chats.filter(x=> x.id==msg.courentchatid).map(x=> x.dialog)[0]
}
PushMsgToUserChatWheneNotSelectContact(msg:any){
    const message = {
        id:msg.id,
        who    : msg.who,
        message: msg.message,
        time   : new Date().toISOString(),
        read:msg.read,    
    };
    if(this.user.id!=message.who){    
    this.chats.filter(x=> x.id==msg.sessionchat )[0].unread=
    this.chats.filter(x=> x.id==msg.sessionchat ).map(x=> x.dialog)[0].filter(x=> x.read==0).length
}
 this.chats.filter(x=> x.id==msg.sessionchat).map(x=> x.dialog)[0].push(message)

}
   /**
     * Create New Chat
     * @param contactId
     * @returns {Promise<any>}
     */
readmsgunred(chatid:number,msgid:number): any{
     
    if(this.chats.filter(x=> x.id==chatid)[0].unread>0){
    this.chats.filter(x=> x.id==chatid)[0].unread-=1}
  return   this.chats.filter(x=> x.id==chatid).map(x=> x.dialog)[0].filter(x=> x.id==msgid)[0].read=1
}
getContactByid( contactid:number){

    return this.contacts.filter(x=> x.id==contactid)[0];
}

AddContactPush(contacts:any){
    

   let chat={
    contact:contacts.contact.filter(x=> x.id!=this.user.id)[0].id,
    dialog:[{
        id:contacts.msgid,
        message:"6/8/2018 10:02:25 PM",
        read:0,
        time:"6/8/2018 10:02:25 PM",
         who:contacts.contact.filter(x=> x.id!=this.user.id)[0].id,

    }],
    id:contacts.chatid,
    my:contacts.contact.filter(x=> x.id==this.user.id)[0].id,
    unread:1,
 
   }
   
  this.contacts.push(contacts.contact.filter(x=> x.id!=this.user.id)[0]),
     this.chats.push(chat)
 
}
setavatar(avatar :string){
this.user.avatar=avatar;
}
}
