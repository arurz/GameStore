import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Company } from '../../../nomenclatures/companies/models/company.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyCreationService {

  readonly url = '/api/admin/companies';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }
  constructor(private http: HttpClient) { }

  createCompany(company: Company): Observable<Company> {
    return this.http.post<Company>(`${this.url}/create`, company, this.httpOptions);
  }
}
