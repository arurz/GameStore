import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { GameDto } from '../../models/game-dto.model';
import { GameCompanyService } from '../../services/game-company.service';

@Component({
  selector: 'app-game-company',
  templateUrl: './game-company.component.html',
  styleUrls: ['./game-company.component.css']
})
export class GameCompanyComponent implements OnInit {

  games: GameDto[] = [];
  constructor(private gameCompanyService: GameCompanyService,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      let id = +params['id'];
    this.getGamesByCompanyId(id);
    });
  }

  getGamesByCompanyId(id: number): void{
    this.gameCompanyService.getGamesByCompanyId(id)
    .subscribe(games => this.games = games);
  }
}
