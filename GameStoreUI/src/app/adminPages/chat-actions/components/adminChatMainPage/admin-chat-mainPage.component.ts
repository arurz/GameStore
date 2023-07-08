import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { ChatUserDto } from '../../models/chat-user-dto.model';
import { ChatService } from '../../services/chat.service';

@Component({
  selector: 'admin-chat-mainPage',
  templateUrl: './admin-chat-mainPage.component.html',
  styleUrls: ['./admin-chat-mainPage.component.css']
})
export class AdminChatMainPageComponent implements OnInit {
  tempText: string = "";
  currentClientId: number = 0;
  users: ChatUserDto[] = [
    // {
    //   userId: 4894156,
    //   username: "4894156"
    // },
    // {
    //   userId: 121241,
    //   username: "121241"
    // },
    // {
    //   userId: 1123123,
    //   username: "1123123"
    // }
  ]
  selectedClientId = new Subject<number>();
  constructor(private router: Router,
    private chatService: ChatService) {
  }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(){
    this.chatService.getAllUsersWithChat()
      .subscribe((users) => {
        this.users = users;
      })
  }

  selectChat(user: ChatUserDto){
    this.unselectAllUsers();
    user.selected = true;
    this.chatService.openUserChat(user.userId);
  }

  redirectToGames(): void {
    this.router.navigate(['/admin']);
  }

  redirectToGenres(): void {
    this.router.navigate(['/admin/genres']);
  }

  redirectToCompanies(): void{
    this.router.navigate(['/admin/companies'])
  }

  unselectAllUsers(){
    this.users.forEach((user) =>{
      user.selected = false;
    })
  }
}
