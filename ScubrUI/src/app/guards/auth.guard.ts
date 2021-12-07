import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { Constants } from "../Constants/constants";
import { User } from "../models/user";

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private router:Router){ }
    canActivate(route: ActivatedRouteSnapshot, state:RouterStateSnapshot):boolean{
        const user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}') as User;
        if(user && user.email){
            return true;
        }
        else{
            this.router.navigate(["login"]);
            return false;
        }
    }
}