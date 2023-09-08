import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Comment } from '../models/comment.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  readonly url = "api/comment/create";
  serverUrl: string;

  constructor(private http: HttpClient) { 
    this.serverUrl = environment.baseURL;
  }

  createComment(comment: Comment): Observable<Comment> {
    return this.http.post<Comment>(`${this.serverUrl}/${this.url}`, comment);
  }
}
