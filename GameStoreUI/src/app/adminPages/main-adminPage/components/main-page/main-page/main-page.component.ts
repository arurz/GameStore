import { Component, OnInit } from '@angular/core';
import { GameNameIdDto } from '../../../models/game-name-id-dto.model';
import { AdminMainPageService } from '../../../services/admin-mainPage.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class AdminMainPageComponent implements OnInit {

  gamesDto: GameNameIdDto[] = [];
  constructor(private adminService: AdminMainPageService) { }

  ngOnInit(): void {
    this.getGameNames();
  }

  getGameNames(): void {
    this.adminService.getGamesDto()
      .subscribe(gamesDto => this.gamesDto = gamesDto);
  }

  deleteGame(id: number, index: number): void {
    this.adminService.deleteGame(id)
      .subscribe(() => {
        this.gamesDto.splice(index, 1);
      });
  }
}
