import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import {HttpClientModule} from '@angular/common/http'
import { AppComponent } from './app.component';
import { LoginModel, UserService } from '../app/services/user/user.service';
import { LoginComponent } from './login/login.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { routes } from './app-routing.module';
import { RegisterComponent } from './register/register.component';
import { AlbumsComponent } from './albums/albums.component';
import { CreateAlbumComponent } from './createAlbum/createAlbum.component';
import { SongComponent } from './song/song.component';


@NgModule({
  declarations: [						
    AppComponent,
    NavbarComponent,
    LoginComponent,
      RegisterComponent,
      AlbumsComponent,
      CreateAlbumComponent,
      SongComponent
   ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
    FormsModule,
    HttpClientModule
  ],
  providers: [
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
