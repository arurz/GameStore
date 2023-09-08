import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EmailDto } from '../../login/models/email-dto.model';
import { PasswordDto } from '../models/password-dto.model';
import { UserSettings } from '../models/user-settings.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  readonly url = "api/user";
  serverUrl: string;
  constructor(private http: HttpClient) { 
    this.serverUrl = environment.baseURL;
  }

  getUserSettings(userId: number): Observable<UserSettings> {
    return this.http.get<UserSettings>(`${this.serverUrl}/${this.url}/${userId}`);
  }

  updateUserSettings(userSettings: UserSettings): Observable<UserSettings> {
    return this.http.put<UserSettings>(`${this.serverUrl}/${this.url}/updateUser`, userSettings);
  }

  changePassword(passwordDto: PasswordDto): Observable<PasswordDto> {
    return this.http.put<PasswordDto>(`${this.serverUrl}/${this.url}/changePassword`, passwordDto);
  }

  restorePassword(emailForRestore: EmailDto): Observable<EmailDto>{
    return this.http.post<EmailDto>(`${this.serverUrl}/${this.url}/restorePassword`, emailForRestore);
  }
}
