import { Component, OnInit } from '@angular/core';
import { UserService } from '../../user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  isLogged: boolean = false;

  constructor(private userService: UserService) {
    this.checkIfLogged();
   }

  ngOnInit(): void {
  }

  checkIfLogged() {
    this.isLogged = this.userService.isLoged();
  }

  logOut() {
    this.userService.logout();
  }
}
