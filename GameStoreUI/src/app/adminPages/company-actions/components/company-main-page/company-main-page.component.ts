import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NomenclatureIdNameDto } from '../../../../nomenclatures/models/nomenclature-id-name-dto.model';
import { CompanyMainPageService } from '../../services/company-main-page.service';

@Component({
  selector: 'app-company-main-page',
  templateUrl: './company-main-page.component.html',
  styleUrls: ['./company-main-page.component.css']
})
export class CompanyMainPageComponent implements OnInit {

  companiesDto: NomenclatureIdNameDto[] = [];
  constructor(private companyMainPageService: CompanyMainPageService,
    private router: Router) { }

  ngOnInit(): void {
    this.getCompanies();
  }

  getCompanies(): void {
    this.companyMainPageService.getCompamiesNames()
      .subscribe(companies => this.companiesDto = companies);
  }

  deleteCompany(id: number, index: number): void {
    this.companyMainPageService.deleteCompany(id).subscribe();
    this.companiesDto.splice(index, 1);
  }

  redirectToGames(): void {
    this.router.navigate(['/admin']);
  }

  redirectToGenres(): void {
    this.router.navigate(['/admin/genres']);
  }

  redirectToChat(): void{
    this.router.navigate(['/admin/chat'])
  }

}
