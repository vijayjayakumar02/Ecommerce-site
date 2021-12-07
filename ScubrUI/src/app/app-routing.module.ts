import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { LayoutRoutingModule } from './layout/layout-routing.module';

const routes: Routes = [
  {path:"home",component:HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes), LayoutRoutingModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
