import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Game } from '../../../gamePages/models/game.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GameActionService {

  readonly url = "/api/admin/create";
  serverUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }
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

  createGame(game: Game): Observable<Game> {
    return this.http.post<Game>(`${this.serverUrl}/${this.url}`, game, this.httpOptions)
      .pipe(
        tap((newGame: Game) => console.log(`created game name = ${newGame.name}`)),
        catchError(this.handleError<Game>('createGame'))
      );
  }

  getCompamiesNames(): Observable<any[]> {
    return this.http.get<any[]>(`${this.serverUrl}/api/admin/companies`)
      .pipe(
        tap(_ => console.log('fetched companies')),
        catchError(this.handleError<any[]>('getCompamiesNames', []))
      );
  }


}
