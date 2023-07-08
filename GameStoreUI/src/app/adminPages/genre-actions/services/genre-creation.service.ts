import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Genre } from '../../../nomenclatures/genres/models/genre.model';

@Injectable({
  providedIn: 'root'
})
export class GenreActionService {

  readonly url = "/api/admin/genres";
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }
  constructor(private http: HttpClient) { }

  createGenre(genre: Genre): Observable<Genre> {
    return this.http.post<Genre>(`${this.url}/create`, genre, this.httpOptions);
  }
}
