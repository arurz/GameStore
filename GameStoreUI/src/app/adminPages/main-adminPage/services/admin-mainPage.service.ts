import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { Game } from '../../../gamePages/models/game.model';
import { GameNameIdDto } from '../models/game-name-id-dto.model';

@Injectable({
  providedIn: 'root'
})
export class AdminMainPageService {

  readonly url = '/api/admin';
  constructor(private http: HttpClient) { }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      console.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    }
  }

  getGame(id: number): Observable<Game> {
    return this.http.get<Game>(`${this.url}/game/${id}`)
      .pipe(
        tap(_ => console.log('fetched game')),
        catchError(this.handleError<Game>('getGame'))
      );
  }

  getGamesDto(): Observable<GameNameIdDto[]> {
    return this.http.get<GameNameIdDto[]>(`${this.url}/names`)
      .pipe(
        tap(_ => console.log('fetched games')),
        catchError(this.handleError<GameNameIdDto[]>('getGamesDto', []))
      );
  }

  deleteGame(id: number): Observable<Game> {
    return this.http.delete<Game>(`${this.url}/delete/${id}`)
      .pipe(
        tap(_ => console.log(`deleted game id = ${id}`)),
        catchError(this.handleError<Game>(`deleteGame`))
      );
  }

  updateGame(game: Game): Observable<any> {
    return this.http.put<any>(`${this.url}/update`, game);
  }
}
