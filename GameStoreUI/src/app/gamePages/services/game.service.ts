import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { GameDto } from '../models/game-dto.model';

import { catchError, map, tap } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Game } from '../models/game.model';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  readonly url = "api/games";
  constructor(private http: HttpClient) { }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      console.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    }
  }

  getGameById(id: number): Observable<Game> {
    return this.http.get<Game>(`${this.url}/${id}`).pipe(
      tap(_ => console.log('fetched game'))
    );
  }

  getGames(): Observable<GameDto[]> {
    return this.http.get<GameDto[]>(this.url)
      .pipe(
        tap(_ => console.log('fetched games')),
        catchError(this.handleError<GameDto[]>('getGames', []))
      );
  }
}