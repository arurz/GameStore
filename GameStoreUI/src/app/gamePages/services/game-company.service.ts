import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { GameDto } from '../models/game-dto.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GameCompanyService {
  readonly url ="api/gamecompany";
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

  getGamesByCompanyId(id: number): Observable<GameDto[]>{
    return this.http.get<GameDto[]>(`${this.serverUrl}/${this.url}/gameByCompany/${id}`)
    .pipe(
      tap(_ => console.log('fetched games by company')),
      catchError(this.handleError<GameDto[]>('getGamesByCompanyId', []))
    );
  }
}
