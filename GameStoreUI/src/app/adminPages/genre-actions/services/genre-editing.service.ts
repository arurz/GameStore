import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { Genre } from '../../../nomenclatures/genres/models/genre.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GenreEditingService {

  readonly url = '/api/admin/genres';
  serverUrl: string;
  constructor(private http: HttpClient) { 
    this.serverUrl = environment.baseURL;
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      console.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    }
  }

  getGenre(id: number): Observable<Genre> {
    return this.http.get<Genre>(`${this.serverUrl}/${this.url}/${id}`)
      .pipe(
        tap(_ => console.log('fetched genre')),
        catchError(this.handleError<Genre>('getGenre'))
      );
  }

  updateGenre(genre: Genre): Observable<any> {
    return this.http.put<any>(`${this.serverUrl}/${this.url}/update`, genre);
  }


}
