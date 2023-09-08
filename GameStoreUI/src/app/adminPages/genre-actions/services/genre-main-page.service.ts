import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Genre } from '../../../nomenclatures/genres/models/genre.model';
import { NomenclatureIdNameDto } from '../../../nomenclatures/models/nomenclature-id-name-dto.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GenreMainPageService {

  readonly url = '/api/admin/genres';
  serverUrl: string;
  constructor(private http: HttpClient) { 
    this.serverUrl = environment.baseURL;
  }

  getGenresNames(): Observable<NomenclatureIdNameDto[]> {
    return this.http.get<NomenclatureIdNameDto[]>(`${this.serverUrl}/${this.url}`);
  }

  getGenresDto(): Observable<any[]> {
    return this.http.get<any[]>(`${this.serverUrl}/${this.url}/dto`);
  }

  deleteGenre(id: number): Observable<Genre> {
    return this.http.delete<Genre>(`${this.serverUrl}/${this.url}/delete/${id}`);
  }

}
