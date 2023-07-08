import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Genre } from '../../../../nomenclatures/genres/models/genre.model';
import { GenreActionService } from '../../services/genre-creation.service';

@Component({
  selector: 'app-genre-action',
  templateUrl: './genre-creation.component.html',
  styleUrls: ['./genre-creation.component.css']
})
export class GenreActionComponent{

  genreForm: Genre = new Genre();
  constructor(private genreActionService: GenreActionService,
    private router: Router) { }

  createGenre() {
    this.genreActionService.createGenre(this.genreForm)
      .subscribe(() =>{
        this.router.navigateByUrl('/admin/genres');
      });
  }
}
