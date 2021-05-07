import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Component, HostBinding, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { albumDetailsModel, albumModel, AlbumService, filteringModel, paginationModel, songModel } from '../services/album/album.service';
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

  currentPageNumber = 1;
  isPreviousPage = false;
  isNextPage = false;
  numberOfPages = 1;

  songData: songModel = {
    id: 0,
    name: '',
    publicationYear: new Date().getFullYear(),
    duration: 0.0
  }

  filteringData: filteringModel = {
    albumTitle: '',
    artistName: '',
    publicationYear: ''
  }

  paginationData: paginationModel = {
    pageSize: 10
  }

  constructor(private userService: UserService, private albumService: AlbumService, private router: Router, private http: HttpClient) { 
    this.token = this.userService.getToken();
    this.isSingleView = false;
    this.isErrorSong = false;
    this.getAllAlbums();
  }

  ngOnInit(): void {
    if (!this.userService.isLoged()) {
      this.router.navigateByUrl('/login');
    }
  }

  getAllAlbums() {
    this.refreshAlbums();
  }

  refreshAlbums() {
    this.albumService.getAlbums(this.filteringData, this.paginationData, this.token, this.currentPageNumber).subscribe(response => {
      const ourHeaders = JSON.parse(response.headers.get('X-Pagination'));
      
      this.isPreviousPage = ourHeaders.HasPrevious;
      this.isNextPage = ourHeaders.HasNext;
      this.numberOfPages = ourHeaders.TotalPages;

      this.albums = [...response.body];
    })
  }

  onFilteringButtonClick() {
    this.currentPageNumber = 1;
    this.isPreviousPage = false;
    this.isNextPage = false;
    this.refreshAlbums();
  }

  onNextPage() {
    this.currentPageNumber++;
    if (this.currentPageNumber === this.numberOfPages) {
      this.isNextPage = false;
    }
    this.refreshAlbums();
  }

  onPreviousPage() {
    this.currentPageNumber--;
    if (this.currentPageNumber === 1) {
      this.isPreviousPage = false;
    }
    this.refreshAlbums();
  }

  onAlbumDetailsButtonClick(album: albumModel) {
    this.albumService.getSingleAlbum(album.id, this.token).subscribe(albumFromService => {
      this.singleAlbumData = albumFromService;
      this.isSingleView = true;
    })
  }

  onDeleteAlbumButtonClick(album: albumModel){
    this.albumService.deleteAlbum(album.id, this.token).subscribe(value => {
      this.refreshAlbums();
    });
  }
}
