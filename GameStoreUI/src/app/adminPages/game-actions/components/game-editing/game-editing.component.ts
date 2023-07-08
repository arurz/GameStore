import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Game } from '../../../../gamePages/models/game.model';
import { AdminMainPageService } from '../../../main-adminPage/services/admin-mainPage.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-game-editing',
  templateUrl: './game-editing.component.html',
  styleUrls: ['./game-editing.component.css']
})
export class GameEditingComponent implements OnInit {

  @Input() game?: Game;

  constructor(private route: ActivatedRoute,
    private adminService: AdminMainPageService,
    private location: Location) { }

  ngOnInit(): void {
    this.getGame();
  }

  getGame(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.adminService.getGame(id)
      .subscribe(game => this.game = game);
  }

  updateGame(): void {
    if (this.game) {
      this.adminService.updateGame(this.game).subscribe(() => this.goBack());
    }
  }

  goBack(): void {
    this.location.back();
  }
}
