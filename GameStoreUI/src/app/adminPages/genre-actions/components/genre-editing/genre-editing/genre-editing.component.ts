import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, ActivationEnd } from '@angular/router';
import { Genre } from '../../../../../nomenclatures/genres/models/genre.model';
import { GenreEditingService } from '../../../services/genre-editing.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-genre-editing',
  templateUrl: './genre-editing.component.html',
  styleUrls: ['./genre-editing.component.css']
})
export class GenreEditingComponent implements OnInit {

  @Input() genre?: Genre;
  constructor(private route: ActivatedRoute,
    private location: Location,
    private genreEditingService: GenreEditingService) { }

  ngOnInit(): void {
    this.getGenre();
  }

  getGenre(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.genreEditingService.getGenre(id)
      .subscribe(genre => this.genre = genre);
  }

  updateGenre(): void {
    if (this.genre) {
      this.genreEditingService.updateGenre(this.genre)
        .subscribe(() => this.goBack());
    }
  }

  goBack(): void {
    this.location.back();
  }

}
