import { Component, HostBinding, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { albumDetailsModel, albumModel, AlbumService, songModel } from '../services/album/album.service';
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

  isErrorSong: boolean;

  songData: songModel = {
    id: 0,
    name: '',
    publicationYear: new Date().getFullYear(),
    duration: 0.0
  }

  constructor(private userService: UserService, private albumService: AlbumService, private router: Router) { 
    this.token = this.userService.getToken();
    this.isSingleView = false;
    this.isErrorSong = false;
    this.getAlbums();
  }

  ngOnInit(): void {
    if (!this.userService.isLoged()) {
      this.router.navigateByUrl('/login');
    }
  }

  getAlbums() {
    this.albumService.getAlbums(this.token).subscribe(albumsFromService => {
      this.albums = albumsFromService;
    })
  }

  onAlbumDetailsButtonClick(album: albumModel) {
    this.albumService.getSingleAlbum(album.id, this.token).subscribe(albumFromService => {
      this.singleAlbumData = albumFromService;
      this.isSingleView = true;
    })
  }

  onCreateAlbumButtonClick(){
    this.router.navigateByUrl('/createAlbum');
  }

  onDeleteAlbumButtonClick(album: albumModel){
    this.albumService.deleteAlbum(album.id, this.token).subscribe(value => {
      window.location.reload();
    });
  }

  onSubmitSong(album: albumDetailsModel) {
    this.albumService.createSong(album.id, this.songData, this.token).subscribe(response => {
      if(response) {
        window.location.reload();
      } else {
        this.isErrorSong = true;
      }
    }, error => {
      this.isErrorSong = true;
    });
  }

  onDeleteSongButtonClick(album: albumDetailsModel, song: songModel){
    this.albumService.deleteSong(album.id, song.id, this.token).subscribe(value => {
      window.location.reload();
    });
  }
}
