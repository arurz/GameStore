import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../../users/register/models/user.model';

@Injectable({
  providedIn: 'root'
})
export class GamePageService {

  constructor(private http: HttpClient) { }

  public getUser(id: number): Observable<User> {
    return this.http.get<User>(`api/user/get/${id}`);
  }
}
