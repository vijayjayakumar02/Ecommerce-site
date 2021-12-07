import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { Constants } from 'src/app/Constants/constants';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private router:Router, private formBuilder: FormBuilder, private userService: UserService){ }

  public loginForm=this.formBuilder.group({
    email:['',[Validators.email,Validators.required]],
    password:['',Validators.required]
  })
  
  ngOnInit(): void {

  }

  login(){
    console.log("login working");
    let email = this.loginForm.controls["email"].value;
    let password = this.loginForm.controls["password"].value;
    this.userService.login(email, password).subscribe((data:any)=>{
      if(data.responseCode == 1){
        console.log(data);
        localStorage.setItem(Constants.USER_KEY, JSON.stringify(data.dataSet));
        this.router.navigate(["home"]);
      }
      console.log("response", data)
    })
  }
}
