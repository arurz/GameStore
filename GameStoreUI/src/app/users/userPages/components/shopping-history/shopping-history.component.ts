import { Component, OnInit } from '@angular/core';
import { CurrentUserDataService } from '../../../current-user-data/current-user-data.service';
import { TokenData } from '../../../current-user-data/models/token-data.enum';
import { CartDto } from '../../models/cart-dto.model';
import { ShoppingDto } from '../../models/shopping-dto.model';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-shopping-history',
  templateUrl: './shopping-history.component.html',
  styleUrls: ['./shopping-history.component.css']
})
export class ShoppingHistoryComponent implements OnInit {

  userId: number = Number(this.currentUserDataService.getTokenData(TokenData.UserId));
  carts: ShoppingDto[] = [];
  soldGames: CartDto[] = [];
  constructor(private cartService: CartService,
    private currentUserDataService: CurrentUserDataService) { }

  ngOnInit(): void {
    this.getSoldGames();
  }

  getSoldGames(): void {
    this.cartService.getGamesForShoppingHistory(this.userId)
      .subscribe(
        res => {
          this.soldGames = res;
        }
      );
  }

}
