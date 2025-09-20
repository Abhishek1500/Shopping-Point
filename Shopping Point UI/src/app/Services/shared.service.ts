import { Injectable } from '@angular/core';
import { Subject, Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  private reloadComponentSubject=new Subject<void>();
  reloadComponent=this.reloadComponentSubject.asObservable();
  constructor() { }

  triggerReload(){
    this.reloadComponentSubject.next();
  }
}
