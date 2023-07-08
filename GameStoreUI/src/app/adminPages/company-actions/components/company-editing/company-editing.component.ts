import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Company } from '../../../../nomenclatures/companies/models/company.model';
import { Location } from '@angular/common';
import { CompanyEditingService } from '../../services/company-editing.service';

@Component({
  selector: 'app-company-editing',
  templateUrl: './company-editing.component.html',
  styleUrls: ['./company-editing.component.css']
})
export class CompanyEditingComponent implements OnInit {

  @Input() company?: Company;
  constructor(private route: ActivatedRoute,
    private location: Location,
    private companyEditingService: CompanyEditingService) { }

  ngOnInit(): void {
    this.getCompany();
  }


  getCompany(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.companyEditingService.getCompany(id)
      .subscribe(company => this.company = company);
  }
  updateCompany(): void {
    if (this.company) {
      this.companyEditingService.updateCompany(this.company)
        .subscribe(() => this.goBack());
    }
  }
  goBack(): void {
    this.location.back();
  }
}
