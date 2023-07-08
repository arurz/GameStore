import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CurrentUserDataService } from '../../../../current-user-data/current-user-data.service';
import { TokenData } from '../../../../current-user-data/models/token-data.enum';
import { PasswordDto } from '../../../models/password-dto.model';
import { SettingsService } from '../../../services/settings.service';

@Component({
  selector: 'app-password-change',
  templateUrl: './password-change.component.html',
  styleUrls: ['./password-change.component.css']
})
export class PasswordChangeComponent{
  passwordDto: PasswordDto = new PasswordDto();

  constructor(private settingsService: SettingsService,
    private currentUserDataService: CurrentUserDataService,
    private router: Router) 
  { }

  changePassword(): void {
    this.passwordDto.id = Number(this.currentUserDataService.getTokenData(TokenData.UserId));
    this.settingsService.changePassword(this.passwordDto)
      .subscribe(() => {
          localStorage.removeItem('token');
          this.router.navigate(['/login']);
        }
      );
  }
}
