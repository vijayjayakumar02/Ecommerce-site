import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Constants } from 'src/app/Constants/constants';
import { User } from 'src/app/models/user';
import { SidenavService } from 'src/app/services/sidenav.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  constructor(private router: Router, private sidenavservice: SidenavService) { }
  onLogout(){
    localStorage.removeItem(Constants.USER_KEY);
  }

  get isUserLoggedIn(){
    const user = localStorage.getItem(Constants.USER_KEY);
    return user && user.length > 0;
  }

  clickMenu(){
    console.log("click menu")
    this.sidenavservice.toggle();
  }
  ngOnInit(): void {
  }

}
