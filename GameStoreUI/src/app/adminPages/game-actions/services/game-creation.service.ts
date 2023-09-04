import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Game } from '../../../gamePages/models/game.model';
import { CompanyDto } from '../../../nomenclatures/companies/models/company-dto.model';

@Injectable({
  providedIn: 'root'
})
export class GameActionService {

  readonly url = "/api/admin/create";
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }
  constructor(private http: HttpClient) { }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      console.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    }
  }

  createGame(game: Game): Observable<Game> {
    return this.http.post<Game>(this.url, game, this.httpOptions)
      .pipe(
        tap((newGame: Game) => console.log(`created game name = ${newGame.name}`)),
        catchError(this.handleError<Game>('createGame'))
      );
  }

  getCompamiesNames(): Observable<any[]> {
    return this.http.get<any[]>('/api/admin/companies')
      .pipe(
        tap(_ => console.log('fetched companies')),
        catchError(this.handleError<any[]>('getCompamiesNames', []))
      );
  }


}
