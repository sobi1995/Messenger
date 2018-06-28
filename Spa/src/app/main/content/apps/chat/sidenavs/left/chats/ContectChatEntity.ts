export class ContectChatEntity{

    Contact:Contact;
    Chat:Chat[];




}

export class Contact{

    avatar:string;
    firstname:string;
    lastname:string;
    mood:string;
    status:string;
    statusenum:number;
    username:string
 
}

export class Chat{

    contact:number;
    dialog:dialog[];
    id:number;
    my:number;
    unread:number;
  



}


export class dialog{




    id:number;
    message:string;
    read:number;
    time:Date;
    who:number
   

}
