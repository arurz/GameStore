import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { forkJoin, Subscription } from 'rxjs';
import { AuthenticationService } from '../../../authorization/services/authentication.service';
import { Company } from '../../../nomenclatures/companies/models/company.model';
import { CompaniesService } from '../../../nomenclatures/companies/services/companies.service';
import { Genre } from '../../../nomenclatures/genres/models/genre.model';
import { GenresService } from '../../../nomenclatures/genres/services/genres.service';
import { CurrentUserDataService } from '../../../users/current-user-data/current-user-data.service';
import { TokenData } from '../../../users/current-user-data/models/token-data.enum';
import { UserRoles } from '../../../users/current-user-data/models/user-roles.enum';
import { LoginEvent } from '../../../users/login/models/login-event.enum';
import { LoginService } from '../../../users/login/services/login.service';
import { MenuItem } from '../../models/menu-item.model';
import { MenuService } from '../../services/menu.service';
@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit, OnDestroy {
  outsideLoginPage: boolean = true;
  loggedIn: boolean;
  title = 'GameStore';
  username: string;

  menuItems: MenuItem;
  gameAttributes: MenuItem;
  genres: Genre[];
  companies: Company[];

  userSettingsSubscription: Subscription;

  constructor(private loginService: LoginService,
    private auth: AuthenticationService,
    private currentUserDataService: CurrentUserDataService,
    private router: Router,
    private menuService: MenuService,
    private genreService: GenresService,
    private companiesService: CompaniesService) 
    {
      if(!this.username){
        this.loggedIn = false;
      }

      this.loginService.subscribe(event =>{
        if(event === LoginEvent.login){
          this.loggedIn = true;
          this.outsideLoginPage = true;

          let role = this.currentUserDataService.getTokenData(TokenData.Role);
          this.menuService.setItems(role);
        }
        else{
          this.outsideLoginPage = false;
        }
      });

      this.userSettingsSubscription = this.menuService.userSettings$.subscribe((userSettings: MenuItem) =>{
        this.menuItems = userSettings;
      })
    }

  ngOnInit(): void {
    let role = this.currentUserDataService.getTokenData(TokenData.Role);
    this.menuService.setItems(role);

    this.getGameAttributes();
    this.checkLogin();
  }
  
  ngOnDestroy(): void {
    this.userSettingsSubscription.unsubscribe();
  }

  checkLogin(): void {
    if (this.auth.loggedIn()) {
      this.username = this.currentUserDataService.getTokenData(TokenData.Username);
      this.loggedIn = true;
    }
    else {
      this.loggedIn = false;
    }
  }

  logout(): void {
    this.loggedIn = false;
    this.loginService.emit(LoginEvent.logout);

    localStorage.removeItem('token');
    this.checkLogin();
    this.router.navigate(['/login']);
  }

  enableAdminButton(){
    let role = this.currentUserDataService.getTokenData(TokenData.Role);

    if(role === UserRoles.Admin){
      return true;
    }
    return false;
  }

  getGameAttributes(){
    forkJoin([
      this.genreService.getGenres(),
      this.companiesService.getCompanies()])
      .subscribe(attributes => {
        this.gameAttributes = {
        isActive: true,
        items: [
          {
            label: "Genre",
            isActive: true,
            items: attributes[0],
            link: "/genregame/gamesByGenre/"
          },
          {
            label: "Companies",
            isActive: true,
            items: attributes[1],
            link: "/gamecompany/gameByCompany/"
          }
        ]
      };
    });
  }
}
