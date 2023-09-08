import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CartDto } from '../models/cart-dto.model';
import { Cart } from '../models/cart.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  readonly url = "api/cart";
  serverUrl: string;
  constructor(private http: HttpClient) { 
    this.serverUrl = environment.baseURL;
  }

  getGames(userId: number): Observable<CartDto[]> {
    return this.http.get<CartDto[]>(`${this.serverUrl}/${this.url}/${userId}`);
  }

  getGamesForShoppingHistory(userId: number): Observable<CartDto[]> {
    return this.http.get<CartDto[]>(`${this.serverUrl}/${this.url}/sold/${userId}`);
  }

  addCart(cart: Cart): Observable<Cart> {
    return this.http.post<Cart>(`${this.serverUrl}/${this.url}/add`, cart);
  }

  deleteCart(cart: Cart): Observable<any> {
    return this.http.delete(`${this.serverUrl}/${this.url}/delete`, { body: { userId: cart.userId, gameId: cart.gameId } });
  }

  buyGames(carts: Cart[]): Observable<Cart[]> {
    return this.http.post<Cart[]>(`${this.serverUrl}/${this.url}/buy`, carts);
  }
}
