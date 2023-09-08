import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { GameDto } from '../models/game-dto.model';

import { catchError, tap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Game } from '../models/game.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  readonly url = "api/game";
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

  getGameById(id: number): Observable<Game> {
    return this.http.get<Game>(`${this.serverUrl}/${this.url}/${id}`).pipe(
      tap(_ => console.log('fetched game'))
    );
  }

  getGames(): Observable<GameDto[]> {
    return this.http.get<GameDto[]>(`${this.serverUrl}/${this.url}`)
      .pipe(
        tap(_ => console.log('fetched games')),
        catchError(this.handleError<GameDto[]>('getGames', []))
      );
  }
}