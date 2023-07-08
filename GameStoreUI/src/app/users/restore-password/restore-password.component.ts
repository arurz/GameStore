import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EmailDto } from '../login/models/email-dto.model';
import { SettingsService } from '../userPages/services/settings.service';

@Component({
  selector: 'app-restore-password',
  templateUrl: './restore-password.component.html',
  styleUrls: ['./restore-password.component.css']
})
export class RestorePasswordComponent implements OnInit {

  emailForRestore: EmailDto = new EmailDto();
  constructor(private settingsService: SettingsService, 
    private router: Router) { }

  ngOnInit(): void {
  }

  restorePassword(){
    this.settingsService.restorePassword(this.emailForRestore)
      .subscribe(() =>{
        this.router.navigate(['/login']);
      })
  }
}
