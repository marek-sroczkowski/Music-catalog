import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginModel, UserService } from '../services/user/user.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  isErrorInLogin: boolean = false;

  loginModel: LoginModel = {
    username: '',
    password: ''
  };

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    console.log(this.loginModel);
    this.userService.login(this.loginModel).subscribe(response => {
      if (response) {
        console.log('good login');
        this.router.navigateByUrl('/');
      } else {
        this.isErrorInLogin = true;
      }
    }, error => {
      this.isErrorInLogin = true;
    });
  }

}
