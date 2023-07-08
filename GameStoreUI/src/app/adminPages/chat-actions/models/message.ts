import { User } from "../../../users/register/models/user.model";

export class Message {
    id: number;
    fromUserId: number;
    fromUser: User;
    toUserId: number;
    creationDate: Date;
    content: string;
}
