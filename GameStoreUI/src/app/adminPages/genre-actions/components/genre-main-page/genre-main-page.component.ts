import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NomenclatureIdNameDto } from '../../../../nomenclatures/models/nomenclature-id-name-dto.model';
import { GenreMainPageService } from '../../services/genre-main-page.service';

@Component({
  selector: 'app-genre-main-page',
  templateUrl: './genre-main-page.component.html',
  styleUrls: ['./genre-main-page.component.css']
})
export class GenreMainPageComponent implements OnInit {

  genresDto: NomenclatureIdNameDto[] = [];
  constructor(private router: Router,
    private genreMainPageService: GenreMainPageService) { }

  ngOnInit(): void {
    this.getGenresNames();
  }

  getGenresNames(): void {
    this.genreMainPageService.getGenresNames()
      .subscribe(genresDto => this.genresDto = genresDto);
  }

  deleteGenre(id: number, index: number): void {
    this.genreMainPageService.deleteGenre(id).subscribe();
    this.genresDto.splice(index, 1);
  }

  redirectToGames(): void {
    this.router.navigate(['/admin']);
  }

  redirectToCompanies(): void {
    this.router.navigate(['/admin/companies']);
  }

  redirectToChat(): void{
    this.router.navigate(['/admin/chat'])
  }

}
