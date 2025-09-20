import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NavprofilesharedService {
  
  constructor() { }
  private newUserData=new Subject<any>();
  private logoutuser=new Subject<void>();

  public logoutStream$=this.logoutuser.asObservable();

  public dataStream$=this.newUserData.asObservable();

  setNewUserDetailInStorage(data:any){
    this.newUserData.next(data);
  }

  SendlogoutInstruction(){
    this.logoutuser.next();
  }

}
