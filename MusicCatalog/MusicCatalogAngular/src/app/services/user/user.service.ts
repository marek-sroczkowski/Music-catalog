import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
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

  JWT_TOKEN = 'JWT_TOKEN'

  islogedin: boolean = false;
  jwtToken: string = '';

  constructor(private http: HttpClient) { }

  register(registerData: RegisterModel): Observable<boolean> {
    return this.http.post('api/account/register/supplier', registerData, { observe: 'response' })
      .pipe(
        map(response => {
          if(response.status === 400) {
            return false;
          }
          console.log(response);
          const loginData: LoginModel = {
            username: registerData.username,
            password: registerData.password
          };
          return true;
        }),
        catchError(error => {
          console.log("error");
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
          console.log(response);
          this.storeToken(response.body!);
          console.log(response.body);
          return true;
        }),
        catchError(error => {
          console.log("error");
          return of(false);
        })
      );
  }

  logout() {
    console.log("removing");
    localStorage.removeItem(this.JWT_TOKEN);
    window.location.reload();
  }

  isLoged(): boolean {
    return !!localStorage.getItem(this.JWT_TOKEN);
  }

  getToken(): string {
    return localStorage.getItem(this.JWT_TOKEN)!;
  }

  private storeToken(token: string) {
    localStorage.setItem(this.JWT_TOKEN, token);
  }
}
