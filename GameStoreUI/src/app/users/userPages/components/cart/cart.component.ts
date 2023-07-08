import { Component, OnInit } from '@angular/core';
import { CurrentUserDataService } from '../../../current-user-data/current-user-data.service';
import { TokenData } from '../../../current-user-data/models/token-data.enum';
import { CartDto } from '../../models/cart-dto.model';
import { Cart } from '../../models/cart.model';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  userId: number = Number(this.currentUserDataService.getTokenData(TokenData.UserId));

  games: CartDto[] = [];
  totalPrice: number = 0;

  cart: Cart = new Cart();

  constructor(private cartService: CartService,
    private currentUserDataService: CurrentUserDataService) { }

  ngOnInit(): void {
    this.getGamesByUserId();
  }

  getGamesByUserId(): void {
    this.cartService.getGames(this.userId)
      .subscribe(
        res => {
          this.games = res;
          this.calculateTotalPrice();
        }
      );
  }

  deleteCart(id: number, index: number): void {
    this.cart.gameId = id;
    this.cart.userId = this.userId;

    this.cartService.deleteCart(this.cart)
      .subscribe(() => {
        this.games.splice(index, 1);
      });
  }

  buyGames(): void {
    let carts: Cart[] = [];

    this.games.forEach(game => {
      let tempCart: Cart = new Cart();
      tempCart.gameId = game.id;
      tempCart.userId = this.userId;

      carts.push(tempCart);
    })
    this.cartService.buyGames(carts)
      .subscribe(() => {
          this.games = [],
          this.totalPrice = 0
        }
      );
  }

  calculateTotalPrice(): void {
    this.games.forEach(g => this.totalPrice += g.price)
    this.totalPrice = Math.round(this.totalPrice * 100) / 100
  }
}
