import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Genre } from '../../../nomenclatures/genres/models/genre.model';
import { NomenclatureIdNameDto } from '../../../nomenclatures/models/nomenclature-id-name-dto.model';

@Injectable({
  providedIn: 'root'
})
export class GenreMainPageService {

  readonly url = '/api/admin/genres';
  constructor(private http: HttpClient) { }

  getGenresNames(): Observable<NomenclatureIdNameDto[]> {
    return this.http.get<NomenclatureIdNameDto[]>(this.url);
  }

  getGenresDto(): Observable<any[]> {
    return this.http.get<any[]>(`${this.url}/dto`);
  }

  deleteGenre(id: number): Observable<Genre> {
    return this.http.delete<Genre>(`${this.url}/delete/${id}`);
  }

}
