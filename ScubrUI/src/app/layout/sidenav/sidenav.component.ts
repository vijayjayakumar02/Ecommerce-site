import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { SidenavService } from 'src/app/services/sidenav.service';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit {
  @ViewChild('sidenav')
  public sidenav!: MatSidenav;

  constructor(private sideNavService: SidenavService) { }

  ngOnInit(){
    this.sideNavService.sideNavToggleSubject.subscribe((data:boolean)=> {
      console.log(data);
      if(data == true){
        this.sidenav.toggle();
      }
    });
  }

}
