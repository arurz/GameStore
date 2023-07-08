import { Component, OnInit } from '@angular/core';
import { GameDto } from '../../models/game-dto.model';
import { GameService } from '../../services/game.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {
  games: GameDto[] = [];

  constructor(private gameService: GameService) { }

  ngOnInit(): void {
    this.getGames();
  }

  reload(): void {
    if (window.localStorage) {
      if (!localStorage.getItem('firstLoad')) {
        localStorage['firstLoad'] = true;
        window.location.reload();
      }
      else
        localStorage.removeItem('firstLoad');
    }
  }

  getGames(): void {
    this.gameService.getGames()
      .subscribe(games => this.games = games);
    //this.reload();
  }
}
