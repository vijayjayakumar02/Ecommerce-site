import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { LayoutComponent } from './layout.component';
import { SearchComponent } from './search/search.component';


import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';


import { LoginComponent } from './login/login.component';
import { LayoutRoutingModule } from './layout-routing.module';
import { UserRegistrationComponent } from './user-registration/user-registration.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SidenavService } from '../services/sidenav.service';
import { SidenavComponent } from './sidenav/sidenav.component';
import { NavlistComponent } from './sidenav/navlist/navlist.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';









@NgModule({
  declarations: [
    NavBarComponent, 
    LayoutComponent, 
    SearchComponent, 
    LoginComponent,
    UserRegistrationComponent,
    SidenavComponent,
    NavlistComponent,
  ],
  imports: [
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    MatCardModule,
    MatFormFieldModule,
    LayoutRoutingModule,
    ReactiveFormsModule,
    MatSidenavModule,
    MatListModule,
    CommonModule
  ],
  exports: [
    LayoutComponent
  ],
  providers:[
    SidenavService
  ]
})
export class LayoutModule { }
