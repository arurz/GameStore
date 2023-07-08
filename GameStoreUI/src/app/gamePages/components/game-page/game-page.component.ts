import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CurrentUserDataService } from '../../../users/current-user-data/current-user-data.service';
import { TokenData } from '../../../users/current-user-data/models/token-data.enum';
import { Cart } from '../../../users/userPages/models/cart.model';
import { CartService } from '../../../users/userPages/services/cart.service';
import { Comment } from '../../models/comment.model';
import { Game } from '../../models/game.model';
import { CommentService } from '../../services/comment.service';

import { GameService } from '../../services/game.service';

@Component({
  selector: 'app-game-page',
  templateUrl: './game-page.component.html',
  styleUrls: ['./game-page.component.css']
})
export class GamePageComponent implements OnInit {
  game: Game = new Game();
  comment: Comment = new Comment();
  tempComment: Comment = new Comment();
  userId: number;

  cart: Cart = new Cart();

  constructor(private gameService: GameService,
    private commentService: CommentService,
    private route: ActivatedRoute,
    private router: Router,
    private cartService: CartService,
    private currentUserDataService: CurrentUserDataService) 
  {
    this.userId =  Number(this.currentUserDataService.getTokenData(TokenData.UserId));
  }

  ngOnInit(): void {
    this.getGameById();
  }

  getGameById(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.gameService.getGameById(id).subscribe(game => this.game = game);
  }

  createComment(): void {
    if (this.comment.content) {

      this.comment.gameId = this.game.id;
      this.comment.userId = this.userId;

      this.commentService.createComment(this.comment)
        .subscribe(
          res => {
            this.tempComment.creationDate = res.creationDate;
            this.tempComment.username = res.username;
            this.tempComment.content = res.content;
            this.comment.content = "";

            this.game.comments.push(this.tempComment);
            this.comment = new Comment();
            this.tempComment = new Comment();
          }
        );
    }
  }

  addGameToCard(): void {
    const gameid = Number(this.route.snapshot.paramMap.get('id'));

    this.cart.gameId = gameid;
    this.cart.userId = this.userId;

    this.cartService.addCart(this.cart)
      .subscribe(() => {
          this.router.navigate(['/games']);
        }
      );
  }
}
