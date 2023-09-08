import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Genre } from '../models/genre.model';
import { environment } from '../../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class GenresService {
  readonly url = "api/genres";
  serverUrl: string;
  constructor(private http: HttpClient) {  
    this.serverUrl = environment.baseURL;
  }

  private handleError<T>(operation = 'operation', result?: T){
    return (error: any): Observable<T> => {
      console.error(error);
      console.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    }
  }
  
  getGenres(): Observable<Genre[]>{
    return this.http.get<Genre[]>(`${this.serverUrl}/${this.url}`)
    .pipe(
      tap(_ => console.log('fetched genres')),
      catchError(this.handleError<Genre[]>('getGenres',[]))
    );
  }
}
