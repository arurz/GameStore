import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { GameActionService } from '../../../adminPages/game-actions/services/game-creation.service';
import { GenreMainPageService } from '../../../adminPages/genre-actions/services/genre-main-page.service';
import { MainPageComponent } from '../../../gamePages/components/main-page/main-page.component';
import { GameDto } from '../../../gamePages/models/game-dto.model';
import { Game } from '../../../gamePages/models/game.model';
import { CompanyDto } from '../../../nomenclatures/companies/models/company-dto.model';
import { GenreDto } from '../../../nomenclatures/genres/models/genre-dto.model';
import { SearchDto } from '../../models/search-dto.model';
import { SearchService } from '../../services/search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  searchDto: SearchDto = new SearchDto();
  games: Game[];

  genresDto: GenreDto[];
  companiesDto: CompanyDto[];

  selectedGenres: GenreDto[];
  selectedCompanies: CompanyDto[];

  dropdownGenreSettings: IDropdownSettings = {};
  dropdownCompanySettings: IDropdownSettings = {};

  constructor(public gameActionService: GameActionService,
    public genreMainPageService: GenreMainPageService,
    public searchService: SearchService,
    // public mainPageComponent: MainPageComponent,
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

  addGenreToDto(): void {
    for (let i = 0; i < this.selectedGenres.length; i++) {
      var genre: number = this.selectedGenres[i].typeId;
      this.searchDto.genres.push(genre);
    }
    this.selectedGenres = [];
  }

  addCompanyToDto(): void {
    for (let i = 0; i < this.selectedCompanies.length; i++) {
      var gameCompany: number = this.selectedCompanies[i].companyId;
      this.searchDto.companies.push(gameCompany);
    }
    this.selectedCompanies = [];
  }

  searchGame(): void {
    if (this.selectedGenres != undefined)
      this.addGenreToDto();
    if (this.selectedCompanies != undefined)
      this.addCompanyToDto();

    this.searchService.search(this.searchDto)
      .subscribe(games => this.games = games);
    this.searchDto = new SearchDto();
  }
}
