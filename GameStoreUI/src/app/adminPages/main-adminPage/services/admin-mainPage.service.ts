import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { Game } from '../../../gamePages/models/game.model';
import { GameNameIdDto } from '../models/game-name-id-dto.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminMainPageService {

  readonly url = '/api/admin';
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

  getGame(id: number): Observable<Game> {
    return this.http.get<Game>(`${this.serverUrl}/${this.url}/game/${id}`)
      .pipe(
        tap(_ => console.log('fetched game')),
        catchError(this.handleError<Game>('getGame'))
      );
  }

  getGamesDto(): Observable<GameNameIdDto[]> {
    return this.http.get<GameNameIdDto[]>(`${this.serverUrl}/${this.url}/names`)
      .pipe(
        tap(_ => console.log('fetched games')),
        catchError(this.handleError<GameNameIdDto[]>('getGamesDto', []))
      );
  }

  deleteGame(id: number): Observable<Game> {
    return this.http.delete<Game>(`${this.serverUrl}/${this.url}/delete/${id}`)
      .pipe(
        tap(_ => console.log(`deleted game id = ${id}`)),
        catchError(this.handleError<Game>(`deleteGame`))
      );
  }

  updateGame(game: Game): Observable<any> {
    return this.http.put<any>(`${this.serverUrl}/${this.url}/update`, game);
  }
}
