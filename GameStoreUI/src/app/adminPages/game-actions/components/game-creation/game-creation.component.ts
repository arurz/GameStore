import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { Game, GameCompany, GameGenre } from '../../../../gamePages/models/game.model';
import { CompanyDto } from '../../../../nomenclatures/companies/models/company-dto.model';
import { GenreDto } from '../../../../nomenclatures/genres/models/genre-dto.model';
import { GenreMainPageService } from '../../../genre-actions/services/genre-main-page.service';
import { GameActionService } from '../../services/game-creation.service';

@Component({
  selector: 'app-game-action',
  templateUrl: './game-creation.component.html',
  styleUrls: ['./game-creation.component.css']
})
export class GameActionComponent implements OnInit {

  genresDto: GenreDto[] = [];
  companiesDto: CompanyDto[] = [];
  selectedGenres: GenreDto[] = [];
  selectedCompanies: CompanyDto[] = [];

  gameForm: Game = new Game();
  dropdownGenreSettings: IDropdownSettings = {};
  dropdownCompanySettings: IDropdownSettings = {};

  fileToUpload: File;

  constructor(public gameActionService: GameActionService,
    public genreMainPageService: GenreMainPageService,
    private router: Router) { }

  ngOnInit(): void {
    this.getGenres();
    this.getCompanies();
    this.dropdownCompanySettings = {
      singleSelection: false,
      idField: "companyId",
      textField: "name",
      selectAllText: "Select All",
      unSelectAllText: "UnSelect All",
      itemsShowLimit: 5,
      allowSearchFilter: true
    };
    this.dropdownGenreSettings = {
      singleSelection: false,
      idField: "typeId",
      textField: "name",
      selectAllText: "Select All",
      unSelectAllText: "UnSelect All",
      itemsShowLimit: 5,
      allowSearchFilter: true
    };
  }

  getGenres(): void {
    this.genreMainPageService.getGenresDto()
      .subscribe(genresDto => this.genresDto = genresDto);
  }

  getCompanies(): void {
    this.gameActionService.getCompamiesNames()
      .subscribe(companiesDto => this.companiesDto = companiesDto);
  }

  handleFileInput(eventData: any) {
    debugger;
    this.fileToUpload = eventData.target.files[0];
  }

  addToGameGenre(): void {
    for (let i = 0; i < this.selectedGenres.length; i++) {
      var gameGenre: GameGenre = { typeId: this.selectedGenres[i].typeId };
      this.gameForm.GameTypes.push(gameGenre);
    }

    this.selectedGenres = [];
  }

  addToGameCompany(): void {
    for (let i = 0; i < this.selectedCompanies.length; i++) {
      var gameCompany: GameCompany = { companyId: this.selectedCompanies[i].companyId };
      this.gameForm.GameCompanies.push(gameCompany);
    }

    this.selectedCompanies = [];
  }

  createGame() {
    this.addToGameGenre();
    this.addToGameCompany();

    this.gameActionService.createGame(this.gameForm)
      .subscribe(() => this.router.navigateByUrl("/admin"));
  }
}

