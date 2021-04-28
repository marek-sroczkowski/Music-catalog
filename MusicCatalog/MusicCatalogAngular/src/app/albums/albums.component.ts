import { Component, OnInit } from '@angular/core';
import { albumDetailsModel, albumModel, AlbumService } from '../services/album/album.service';
import { UserService } from '../services/user/user.service';

@Component({
  selector: 'app-albums',
  templateUrl: './albums.component.html',
  styleUrls: ['./albums.component.css']
})
export class AlbumsComponent implements OnInit {

  albums!: albumModel[];
  singleAlbumData!: albumDetailsModel;
  isSingleView: boolean;
  token: string;

  constructor(private userService: UserService, private albumService: AlbumService) { 
    this.token = this.userService.getToken();
    this.isSingleView = false;
    this.getData();
  }

  ngOnInit(): void {
  }

  getData() {
    this.albumService.getAlbums(this.token).subscribe(albumsFromService => {
      this.albums = albumsFromService;
    })
  }

  buttonClick(album: albumModel) {
    this.albumService.getSingleAlbum(album.id, this.token).subscribe(albumFromService => {
      this.singleAlbumData = albumFromService;
      this.isSingleView = true;
    })
  }
}
