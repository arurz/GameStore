import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { RegisterService } from '../services/register.service';
import { User } from '../models/user.model';
import { AuthenticationService } from '../../../authorization/services/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  userForm: User = new User();
  constructor(public registerService: RegisterService,
    private router: Router,
    private auth: AuthenticationService) { }

  ngOnInit(): void {
  }

  register() {
    this.registerService.registerUser(this.userForm)
      .subscribe(
        res => {
          if (res)
            this.router.navigateByUrl('login');
        },
        err => { console.log(err); }
      );
  }
}
