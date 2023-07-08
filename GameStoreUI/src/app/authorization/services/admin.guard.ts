import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { CurrentUserDataService } from '../../users/current-user-data/current-user-data.service';
import { TokenData } from '../../users/current-user-data/models/token-data.enum';
import { UserRoles } from '../../users/current-user-data/models/user-roles.enum';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  constructor(private currentUserDataService: CurrentUserDataService,
    private router: Router) { }

  canActivate(): boolean {
    let userRole = this.currentUserDataService.getTokenData(TokenData.Role);
    
    if(userRole == UserRoles.Admin){
      return true;
    }

    this.router.navigate(['/games']);
    return false;
  }
}
