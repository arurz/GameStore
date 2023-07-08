import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CartDto } from '../models/cart-dto.model';
import { Cart } from '../models/cart.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  readonly url = "api/cart";

  constructor(private http: HttpClient) { }

  getGames(userId: number): Observable<CartDto[]> {
    return this.http.get<CartDto[]>(`${this.url}/${userId}`);
  }

  getGamesForShoppingHistory(userId: number): Observable<CartDto[]> {
    return this.http.get<CartDto[]>(`${this.url}/sold/${userId}`);
  }

  addCart(cart: Cart): Observable<Cart> {
    return this.http.post<Cart>(`${this.url}/add`, cart);
  }

  deleteCart(cart: Cart): Observable<any> {
    return this.http.delete(`${this.url}/delete`, { body: { userId: cart.userId, gameId: cart.gameId } });
  }

  buyGames(carts: Cart[]): Observable<Cart[]> {
    return this.http.post<Cart[]>(`${this.url}/buy`, carts);
  }
}
