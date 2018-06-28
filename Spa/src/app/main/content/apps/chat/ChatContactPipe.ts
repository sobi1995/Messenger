import {Pipe, PipeTransform} from "@angular/core";
import { hub } from "./hub";
import { ChatService } from "./chat.service";
import { useridintity } from "../../../../core/services/ApiAddress";
 
@Pipe({
    name: 'ChatContactPipe'
})
export class ChatContactPipe implements PipeTransform{
   
    constructor(){

        
  
    }

   
    transform(value: any,cat:any,xax:any){
   
     
 
    
      
         
        return value;
    }
}