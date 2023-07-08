import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { Company } from '../../../../nomenclatures/companies/models/company.model';
import { CompanyCreationService } from '../../services/company-creation.service';

@Component({
  selector: 'app-company-actions',
  templateUrl: './company-creation.component.html',
  styleUrls: ['./company-creation.component.css']
})
export class CompanyActionsComponent implements OnInit {

  companyForm: Company = new Company();
  constructor(private companyActionService: CompanyCreationService,
    private router: Router) { }

  ngOnInit(): void {
  }

  createCompany() {
    this.companyActionService.createCompany(this.companyForm)
      .subscribe();
    this.router.navigateByUrl('/admin/companies');
  }
}
