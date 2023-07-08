import { Component } from '@angular/core';
import { LoginService } from '../services/login.service';
import { LoginDto } from '../models/login-dto.model';
import { LoginEvent } from '../models/login-event.enum';
import { CurrentUserDataService } from '../../current-user-data/current-user-data.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  dto: LoginDto = new LoginDto();

  constructor(public loginService: LoginService,
  private currentUserDataService: CurrentUserDataService) 
  {
    this.loginService.emit(LoginEvent.logout);
  }

  login() {
    this.currentUserDataService.loadToken(this.dto);
  }
}
