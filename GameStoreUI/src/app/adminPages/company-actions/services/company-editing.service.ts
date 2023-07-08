import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Company } from '../../../nomenclatures/companies/models/company.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyEditingService {

  readonly url = 'api/admin/companies';
  constructor(private http: HttpClient) { }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      console.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    }
  }

  getCompany(id: number): Observable<Company> {
    return this.http.get<Company>(`${this.url}/${id}`)
      .pipe(
        tap(_ => console.log('fetched company')),
        catchError(this.handleError<Company>('getCompany'))
      );
  }

  updateCompany(company: Company): Observable<any> {
    return this.http.put<any>(`${this.url}/update`, company);
  }
}
