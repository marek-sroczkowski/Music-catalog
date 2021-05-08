import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of, pipe } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

export interface RegisterModel {
  name: string;
  username: string;
  password: string;
  confirmPassword: string;
}

export interface LoginModel {
  username: string,
  password: string
}

@Injectable({
  providedIn: 'root'
})

export class UserService {

  isLoggedIn: boolean = false;
  jwtToken: string = '';

  constructor(private http: HttpClient, private router: Router) { }

  register(registerData: RegisterModel): Observable<boolean> {
    return this.http.post('api/account/register/supplier', registerData, { observe: 'response' })
      .pipe(
        map(response => {
          if(response.status === 400) {
            return false;
          }
          const loginData: LoginModel = {
            username: registerData.username,
            password: registerData.password
          };
          return true;
        }),
        catchError(error => {
          return of(false);
        })
      );
  }

  login(loginData: LoginModel): Observable<boolean> {
    return this.http.post('api/account/login', loginData, { observe: 'response', responseType: 'text' })
      .pipe(
        map(response => {
          if (response.status === 400)
            return false;
          this.storeToken(response.body!);
          return true;
        }),
        catchError(error => {;
          return of(false);
        })
      );
  }

  logout() {
    localStorage.removeItem(this.jwtToken);
    this.router.navigateByUrl('/login');
  }

  isLoged(): boolean {
    return !!localStorage.getItem(this.jwtToken);
  }

  getToken(): string {
    return localStorage.getItem(this.jwtToken)!;
  }

  private storeToken(token: string) {
    localStorage.setItem(this.jwtToken, token);
  }
}
