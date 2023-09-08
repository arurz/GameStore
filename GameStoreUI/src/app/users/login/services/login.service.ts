import { EventEmitter, Injectable } from '@angular/core';
import { LoginEvent } from '../models/login-event.enum';

@Injectable({
  providedIn: 'root'
})

export class LoginService {
  isLogged: boolean;
  emitter = new EventEmitter<LoginEvent>();
  
  subscribe(next: (event: LoginEvent) => void) {
	this.emitter.subscribe(next);
  }

  emit(event: LoginEvent) {
	this.isLogged = event === LoginEvent.login;
	this.emitter.emit(event);
  }
}
