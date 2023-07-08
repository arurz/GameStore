import { Injectable } from "@angular/core";
import { UserRoles } from "./user-roles.enum";

@Injectable()
export class UserContext {
    userName: string;
    userId: number;
    userPernission: UserRoles;
}
