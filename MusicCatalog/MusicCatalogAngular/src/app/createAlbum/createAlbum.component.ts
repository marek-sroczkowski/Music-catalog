import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { albumDetailsModel, AlbumService } from '../services/album/album.service';
import { UserService } from '../services/user/user.service';

@Component({
  selector: 'app-createAlbum',
  templateUrl: './createAlbum.component.html',
  styleUrls: ['./createAlbum.component.css']
})
export class CreateAlbumComponent implements OnInit {

  token: string;
  isErrorAlbum: boolean;

  albumData: albumDetailsModel = {
    id: 0,
    artist: {name: ''},
    publicationYear: new Date().getFullYear(),
    title: '',
    songs: [],
    version:'',
    supplier: ''
  }

  constructor(private userService: UserService, private albumService: AlbumService, private router: Router) { 
    this.token = this.userService.getToken();
    this.isErrorAlbum = false;
  }

  ngOnInit() {
    if (!this.userService.isLoged()) {
      this.router.navigateByUrl('/login');
    }
  }

  onSubmitAlbum() {
    this.albumService.createAlbum(this.albumData, this.token).subscribe(response => {
      if(response) {
        this.router.navigateByUrl('/albums');
      } else {
        this.isErrorAlbum = true;
      }
    }, error => {
      this.isErrorAlbum = true;
    });
  }
}
