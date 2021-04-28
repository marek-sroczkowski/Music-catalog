import { Injectable } from '@angular/core';
import { Observable, of, pipe } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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
  title: string,
  publicationYear: number,
  version: string,
  supplier: string,
  songs: songModel[],
  artist: artistModel
}

@Injectable({
  providedIn: 'root'
})

export class AlbumService {

  constructor(private http: HttpClient) { }

  getAlbums(token: string): Observable<albumModel[]>{
    let headers: HttpHeaders = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<albumModel[]>('api/album', {headers: headers});
  }

  getSingleAlbum(id: number, token: string): Observable<albumDetailsModel> {
    let headers: HttpHeaders = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<albumDetailsModel>(`api/album/${id}`, {headers: headers});
  }
}
