import { Injectable } from '@angular/core';
import { User } from '../models/user.model';

import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  readonly Url = '/api/register';
  serverUrl: string;
  constructor(private http: HttpClient) { 
    this.serverUrl = environment.baseURL;
  }

  registerUser(form: User) {
    return this.http.post<any>(`${this.serverUrl}/${this.Url}`, form);
  }
}