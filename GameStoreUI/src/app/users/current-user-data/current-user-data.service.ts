import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { LoginDto } from '../login/models/login-dto.model';
import { LoginEvent } from '../login/models/login-event.enum';
import { LoginService } from '../login/services/login.service';
import { UserContext } from './models/user-context.model';
import { UserRoles } from './models/user-roles.enum';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CurrentUserDataService {
  serverUrl: string;
  constructor(private userContext: UserContext,
    private http: HttpClient, 
    private router: Router,
    private loginService: LoginService) { 
      this.serverUrl = environment.baseURL;
    }

  loadToken(form: LoginDto){
    return this.http.post<any>(`${this.serverUrl}/api/login`, form).subscribe((res) =>{
      localStorage.setItem("token", res.token);
      this.loginService.emit(LoginEvent.login);

      let decodedJwt = JSON.parse(window.atob(res.token.split('.')[1]))
      this.userContext.userPernission = decodedJwt.role;
      this.userContext.userId = decodedJwt.userId;
      this.userContext.userName = decodedJwt.sub;

      if (decodedJwt.role == UserRoles.Admin) {
        this.router.navigateByUrl('admin');
      }
      else {
        this.router.navigateByUrl('games');
      }
    })
  }

  getTokenData(key: string){
    let token: any = localStorage.getItem("token");
    if(token){
      let decodedToken = JSON.parse(window.atob(token.split('.')[1]));
      return decodedToken[key];
    }
    return null;
  }
}
