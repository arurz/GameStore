import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Genre } from '../../../nomenclatures/genres/models/genre.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GenreActionService {

  readonly url = "/api/admin/genres";
  serverUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }
  constructor(private http: HttpClient) { 
    this.serverUrl = environment.baseURL;
  }

  createGenre(genre: Genre): Observable<Genre> {
    return this.http.post<Genre>(`${this.serverUrl}/${this.url}/create`, genre, this.httpOptions);
  }
}
