import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { UserRoles } from '../../users/current-user-data/models/user-roles.enum';
import { MenuItem } from '../models/menu-item.model';

@Injectable({
  providedIn: "root"
})
export class MenuService {
  userMenuSettings = new Subject<MenuItem>();
  userSettings$ = this.userMenuSettings.asObservable();

  setItems(role: UserRoles){
    let hasAdminAccess: boolean;
    if(role == UserRoles.Admin){
      hasAdminAccess = true;
    }
    else{
      hasAdminAccess = false;
    }
    this.userMenuSettings.next({
      isActive: true,
      label: 'fa fa-user',
      items: [
        {
          label: "Admin Page",
          routerLink: "/admin",
          isActive: hasAdminAccess
        },
        {
          label: "Settings",
          routerLink: "/settings",
          isActive: true
        },
        {
          label: "My Cart",
          routerLink: "/cart",
          isActive: true
        },
        {
          label: "Shopping History",
          routerLink: "/history",
          isActive: true
        }
      ]
    });
  }
}
