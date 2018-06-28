import { FuseUtils } from "../../../../../../core/fuseUtils";

 

export class Contact
{
    my: number;
    nickname: string;
    notes: string;
    phone:string
    constructor( my: number,
        nickname: string,
        notes: string,
        phone:string)
    {
        {
            this.my = my || 0;
            this.nickname = nickname || '';
            this.notes = notes || '';
            this.phone = phone || '';
        }
    }
}
