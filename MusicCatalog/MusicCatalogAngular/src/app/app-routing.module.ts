import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AlbumsComponent } from './albums/albums.component';
import { CreateAlbumComponent } from './createAlbum/createAlbum.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';


export const routes: Routes = [
  {
    path: '',
    component: AlbumsComponent
  },
  {
    path: 'login', 
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'albums',
    component: AlbumsComponent
  },
  {
    path: 'createAlbum',
    component: CreateAlbumComponent
  }
];
