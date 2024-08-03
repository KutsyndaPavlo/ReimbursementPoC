import { ProgramAddComponent }   from "./add/add.component";
  
export const exitGuard=(component: ProgramAddComponent) =>{
      
    if(!component.saved){
        return confirm("Do you want to leave a page?");
    }
    return true;
}