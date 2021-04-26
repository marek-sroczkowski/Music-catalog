import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginModel, RegisterModel, UserService } from '../services/user/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerData: RegisterModel = {
    username: '',
    name: '',
    password: '',
    confirmPassword: ''
  };

  isErrorInRegister: boolean = false;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    console.log(this.registerData);
    this.userService.register(this.registerData).subscribe(response => {
      if (response) {
        console.log('good login');
        this.router.navigateByUrl('/login');
      } else {
        this.isErrorInRegister = true;
      }
    }, error => {
      this.isErrorInRegister = true;
    });
  }

}
