export class UserRegisterDto{

            LastName:string;
            FirstName :string
        
            Password :string
            Mobile:string

            constructor( 
                FirstName :string,
             
                Password :string,
                Mobile:string,
                LastName:string){
                    this.LastName=LastName;
                    this.FirstName=FirstName;
                   
                    this.Password=Password;
                    this.Mobile=Mobile;
                }
}