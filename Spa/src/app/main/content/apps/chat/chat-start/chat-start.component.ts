import { Component, OnInit } from '@angular/core';
import { fuseAnimations } from '../../../../../core/animations';

@Component({
    selector   : 'fuse-chat-start',
    templateUrl: './chat-start.component.html',
    styleUrls  : ['./chat-start.component.scss'],
    animations : fuseAnimations
})
export class FuseChatStartComponent implements OnInit
{

    constructor()
    {
    }

    ngOnInit()
    {
    }

}
