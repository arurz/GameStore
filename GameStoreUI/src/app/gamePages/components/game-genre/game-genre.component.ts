import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { GameDto } from '../../models/game-dto.model';
import { GameGenreService } from '../../services/game-genre.service';

@Component({
  selector: 'app-game-genre',
  templateUrl: './game-genre.component.html',
  styleUrls: ['./game-genre.component.css']
})
export class GameGenreComponent implements OnInit {

  games: GameDto[] = [];
  constructor(private gameGenreService: GameGenreService,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      let id = +params['id'];
    this.getGamesByGenreId(id);
    });
  }

  getGamesByGenreId(id: number): void{
    this.gameGenreService.getGamesByGenreId(id)
    .subscribe(games => this.games = games);
  }
}
