import { Component, OnInit } from '@angular/core';
import { CurrentUserDataService } from '../../../current-user-data/current-user-data.service';
import { TokenData } from '../../../current-user-data/models/token-data.enum';
import { UserSettings } from '../../models/user-settings.model';
import { SettingsService } from '../../services/settings.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {
  userSettings: UserSettings = new UserSettings();
  userId: number = Number(this.currentUserDataService.getTokenData(TokenData.UserId));

  constructor(private settingsService: SettingsService,
    private currentUserDataService: CurrentUserDataService) { }

  ngOnInit(): void {
    this.getUserSettings();
  }

  getUserSettings(): void {
    this.settingsService.getUserSettings(this.userId)
      .subscribe(userSettings => this.userSettings = userSettings);
  }

  updateSettings(): void {
    this.settingsService.updateUserSettings(this.userSettings)
      .subscribe();
  }
}
