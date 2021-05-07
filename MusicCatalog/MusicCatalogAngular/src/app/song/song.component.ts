import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { albumDetailsModel, albumModel, AlbumService, songModel } from '../services/album/album.service';
import { UserService } from '../services/user/user.service';

@Component({
  selector: 'app-song',
  templateUrl: './song.component.html',
  styleUrls: ['./song.component.css']
})
export class SongComponent implements OnInit {

  @Input() album: albumDetailsModel = {
    id: 0,
    title: '',
    publicationYear: 0,
    version: '',
    supplier: '',
    songs: [],
    artist: {
      name: ''
    }
  };
  
  @Output() back: EventEmitter<any> = new EventEmitter;
  token: string;
  isErrorSong: boolean;
  songData: songModel = {
    id: 0,
    name: '',
    publicationYear: new Date().getFullYear(),
    duration: 0.0
  }


  constructor(private userService: UserService, private albumService: AlbumService) { 
    this.token = this.userService.getToken();
    this.isErrorSong = false;
  }

  ngOnInit(): void {
  }

  clearSongFormData(){
    this.songData.name = '',
    this.songData.publicationYear = new Date().getFullYear(),
    this.songData.duration = 0.0
  }

  onSubmitSong(album: albumDetailsModel) {
    this.albumService.createSong(album.id, this.songData, this.token).subscribe(response => {
      if(response) {
        this.albumService.getSingleAlbum(album.id, this.token).subscribe(response => {
          this.album.songs = [...response.songs];
          this.clearSongFormData();
        });
      } else {
        this.isErrorSong = true;
      }
    }, error => {
      this.isErrorSong = true;
    });
  }

  onDeleteSongButtonClick(album: albumDetailsModel, song: songModel){
    this.albumService.deleteSong(album.id, song.id, this.token).subscribe(resp => {
        this.albumService.getSingleAlbum(album.id, this.token).subscribe(response => {
        this.album.songs = [...response.songs];
      });
    })
}

}
