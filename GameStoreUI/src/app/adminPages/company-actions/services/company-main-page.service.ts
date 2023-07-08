import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Company } from '../../../nomenclatures/companies/models/company.model';
import { NomenclatureIdNameDto } from '../../../nomenclatures/models/nomenclature-id-name-dto.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyMainPageService {

  readonly url = '/api/admin/companies';
  constructor(private http: HttpClient) { }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      console.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    }
  }

  getCompamiesNames(): Observable<NomenclatureIdNameDto[]> {
    return this.http.get<NomenclatureIdNameDto[]>(this.url)
      .pipe(
        tap(_ => console.log('fetched genres')),
        catchError(this.handleError<NomenclatureIdNameDto[]>('getCompamiesNames', []))
      );
  }

  deleteCompany(id: number): Observable<Company> {
    return this.http.delete<Company>(`${this.url}/delete/${id}`)
      .pipe(
        tap(_ => console.log(`deleted company id =${id}`)),
        catchError(this.handleError<Company>(`deleteCompany`))
      );
  }
}
