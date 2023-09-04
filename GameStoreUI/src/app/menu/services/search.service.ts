import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Game } from '../../gamePages/models/game.model';
import { CompanyDto } from '../../nomenclatures/companies/models/company-dto.model';
import { GenreDto } from '../../nomenclatures/genres/models/genre-dto.model';
import { SearchDto } from '../models/search-dto.model';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(private http: HttpClient) { }

  private composeQueryString(object: any): string {
    let result = '';
    let isFirst = true;
    if (object) {
      Object.keys(object)
        .filter(key => object[key] !== null && object[key] !== undefined)
        .forEach(key => {
          let value = object[key];
          if (value instanceof Date) {
            value = value.toISOString();
          }
          if (isFirst) {
            result = '?' + key + '=' + value;
            isFirst = false;
          } else {
            result += '&' + key + '=' + value;
          }
        });
    }
    return result;
  }

  public search(searchDto: SearchDto): Observable<Game[]> {
    let query = "/api/game/getGamesByParametres";
    query += this.composeQueryString(searchDto);
    return this.http.get<Game[]>(query);
  }
}
