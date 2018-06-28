import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ChatService } from './chat.service';
import { fuseAnimations } from '../../../../core/animations';
import { hub } from './hub';

@Component({
    selector     : 'fuse-chat',
    templateUrl  : './chat.component.html',
    styleUrls    : ['./chat.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations   : fuseAnimations
})
export class FuseChatComponent implements OnInit
{
    selectedChat: any;

    constructor(private chatService: ChatService,private _hub: hub)
    {
     
    }

    ngOnInit()
    {
          
        this.chatService.onChatSelected
            .subscribe(chatData => {
              
                this.selectedChat = chatData;
            });
    }

}
