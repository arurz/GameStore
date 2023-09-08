import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../../users/register/models/user.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GamePageService {
  serverUrl: string;
  constructor(private http: HttpClient) { 
    this.serverUrl = environment.baseURL;
  }

  public getUser(id: number): Observable<User> {
    return this.http.get<User>(`${this.serverUrl}/api/user/get/${id}`);
  }
}
