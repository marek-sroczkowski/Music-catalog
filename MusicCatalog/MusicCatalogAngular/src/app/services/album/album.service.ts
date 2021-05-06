import { Injectable } from '@angular/core';
import { Observable, of, pipe } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';

export interface artistModel {
  name: string
}

export interface songModel {
  id: number,
  name: string,
  publicationYear: number,
  duration: number
}

export interface albumModel {
  id: number,
  title: string,
  publicationYear: number,
  version: string,
  supplier: string,
  artist: artistModel
}

export interface albumDetailsModel {
  id: number,
  title: string,
  publicationYear: number,
  version: string,
  supplier: string,
  songs: songModel[],
  artist: artistModel
}

export interface filteringModel {
  albumTitle: string,
  artistName: string,
  publicationYear: number
}

@Injectable({
  providedIn: 'root'
})

export class AlbumService {

  constructor(private http: HttpClient) { }

  getAllAlbums(token: string): Observable<albumModel[]>{
    let headers: HttpHeaders = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<albumModel[]>('api/album', {headers: headers});
  }

  getAlbums(filteringData: filteringModel, token: string): Observable<albumModel[]>{
    let headers: HttpHeaders = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    let params: HttpParams = new HttpParams().set('title', filteringData.albumTitle)
                                              .set('artistName', filteringData.artistName)
                                              .set('publicationYear', filteringData.publicationYear.toString());
    return this.http.get<albumModel[]>('api/album', {headers: headers, params: params});
  }

  getSingleAlbum(id: number, token: string): Observable<albumDetailsModel> {
    let headers: HttpHeaders = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<albumDetailsModel>(`api/album/${id}`, {headers: headers});
  }

  createAlbum(album: albumDetailsModel, token: string): Observable<boolean> {
    let headers: HttpHeaders = new HttpHeaders().set('Authorization', `Bearer ${token}`); 
    return this.http.post('api/album', album, {headers: headers, observe: 'response'})
    .pipe(
      map(response => {
        if(response.status === 400)
          return false;
        return true;
      }),
      catchError(error => {
        return of(false);
      })
    );
  }

  deleteAlbum(id: number, token: string): Observable<any> {
    let headers: HttpHeaders = new HttpHeaders().set('Authorization', `Bearer ${token}`); 
    return this.http.delete(`api/album/${id}`, {headers: headers});
  }

  createSong(albumId: number, song: songModel, token: string): Observable<boolean> {
    let headers: HttpHeaders = new HttpHeaders().set('Authorization', `Bearer ${token}`); 
    return this.http.post(`api/album/${albumId}/song`, song, {headers: headers, observe: 'response'})
    .pipe(
      map(response => {
        if(response.status === 400)
          return false;
        return true;
      }),
      catchError(error => {
        return of(false);
      })
    );
  }

  deleteSong(albumId: number, songId: number, token: string): Observable<any> {
    let headers: HttpHeaders = new HttpHeaders().set('Authorization', `Bearer ${token}`); 
    return this.http.delete(`api/album/${albumId}/song/${songId}`, {headers: headers});
  }
}
