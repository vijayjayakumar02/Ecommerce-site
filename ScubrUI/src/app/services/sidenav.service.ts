import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SidenavService {
  public sideNavToggleSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  constructor() { }

  public toggle() {
    console.log("toggle");
    return this.sideNavToggleSubject.next(true);
  } 
}
