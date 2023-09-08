import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { GameDto } from '../models/game-dto.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GameGenreService {
  readonly url ="api/genregame";
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

  getGamesByGenreId(id: number): Observable<GameDto[]>{
    return this.http.get<GameDto[]>(`${this.serverUrl}/${this.url}/gamesByGenre/${id}`)
    .pipe(
      tap(_ => console.log('fetched games by genre')),
      catchError(this.handleError<GameDto[]>('getGamesByGenreId', []))
    );
  }
}
