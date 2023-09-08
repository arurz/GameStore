import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Company } from '../../../nomenclatures/companies/models/company.model';
import { NomenclatureIdNameDto } from '../../../nomenclatures/models/nomenclature-id-name-dto.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CompanyMainPageService {

  readonly url = '/api/admin/companies';
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

  getCompamiesNames(): Observable<NomenclatureIdNameDto[]> {
    return this.http.get<NomenclatureIdNameDto[]>(`${this.serverUrl}/${this.url}`)
      .pipe(
        tap(_ => console.log('fetched genres')),
        catchError(this.handleError<NomenclatureIdNameDto[]>('getCompamiesNames', []))
      );
  }

  deleteCompany(id: number): Observable<Company> {
    return this.http.delete<Company>(`${this.serverUrl}/${this.url}/delete/${id}`)
      .pipe(
        tap(_ => console.log(`deleted company id =${id}`)),
        catchError(this.handleError<Company>(`deleteCompany`))
      );
  }
}
