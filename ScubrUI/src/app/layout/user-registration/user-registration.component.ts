import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.scss']
})
export class UserRegistrationComponent implements OnInit {

  public RegisterForm=this.formBuilder.group({
    fullname:['',Validators.required],
    email:['',[Validators.email,Validators.required]],
    password:['',Validators.required]
  })


  constructor(private router:Router,private formBuilder: FormBuilder,private userService:UserService) { }

  ngOnInit(): void {
  }

  onSubmit(){
    console.log("submit");
    let fullname = this.RegisterForm.controls["fullname"].value;
    let email = this.RegisterForm.controls["email"].value;
    let password = this.RegisterForm.controls["password"].value;
    this.userService.Register(fullname,email,password).subscribe((data)=>{
      console.log("response",data);
    },error=>{
      console.log("error",error)
    })
  }
  
}
