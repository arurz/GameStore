import { Injectable } from '@angular/core';
import { User } from '../models/user.model';

import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  readonly Url = '/api/register';
  constructor(private http: HttpClient) { }

  registerUser(form: User) {
    return this.http.post<any>(this.Url, form);
  }
}