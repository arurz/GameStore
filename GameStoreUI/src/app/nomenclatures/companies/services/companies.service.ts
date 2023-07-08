import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Company } from '../models/company.model';

@Injectable({
  providedIn: 'root'
})
export class CompaniesService {
  readonly url = "api/companies";
  constructor(private http: HttpClient) { }

  private handleError<T>(operation = 'operation', result?: T){
    return (error: any): Observable<T> => {
      console.error(error);
      console.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    }
  }

  getCompanies():Observable<Company[]>{
    return this.http.get<Company[]>(this.url)
    .pipe(
      tap(_ => console.log('fetched companies')),
      catchError(this.handleError<Company[]>('getCompanies', []))
    );
  }
}
