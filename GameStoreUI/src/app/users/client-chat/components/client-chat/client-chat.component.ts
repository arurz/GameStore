import { Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';
import { Message } from '../../../../adminPages/chat-actions/models/message';
import { MessageDto } from '../../../../adminPages/chat-actions/models/message-dto.model';
import { ChatService } from '../../../../adminPages/chat-actions/services/chat.service';
import { CurrentUserDataService } from '../../../current-user-data/current-user-data.service';
import { TokenData } from '../../../current-user-data/models/token-data.enum';
import { UserRoles } from '../../../current-user-data/models/user-roles.enum';

@Component({
  selector: 'client-chat',
  templateUrl: './client-chat.component.html',
  styleUrls: ['./client-chat.component.css']
})
export class ClientChatComponent implements OnInit {
  supportAdminId: number;
  messages: Message[] = [];  
  messageDto: MessageDto = new MessageDto(); 
  isChatEnabled: boolean = false;
  clientId: number;

  @ViewChild('scrollY') private scrollContainer: ElementRef;
  constructor(private currentUserDataService: CurrentUserDataService,
    private chatService: ChatService) 
  { 
    let role = this.currentUserDataService.getTokenData(TokenData.Role);
    if(role == UserRoles.User){
      this.isChatEnabled = true;
    }
    else{
      this.isChatEnabled = false;
    }
    
    this.clientId = this.currentUserDataService.getTokenData(TokenData.UserId);
    this.messageDto.fromUserId = this.clientId;
    this.supportAdminId = 11;
  }
  
  @HostListener('keydown.enter')
  handleEnterButton() {
    this.sendMessage();
  }

  ngAfterViewChecked() {        
    this.scrollToBottom();        
  } 

  scrollToBottom(): void {
    if(this.isChatEnabled)
      this.scrollContainer.nativeElement.scrollTop = this.scrollContainer.nativeElement.scrollHeight;
  }

  ngOnInit(): void {
    this.chatService.retrieveMappedObject()
      .subscribe((acceptedMessage: Message) => { 
        if(this.clientId == acceptedMessage.toUserId || this.clientId == acceptedMessage.fromUserId){
          this.messages.push(acceptedMessage); 
          this.scrollToBottom();
        }
      });
  }

  sendMessage(){
    if(this.messageDto.content.length > 0){
      this.chatService.broadcastMessage(this.messageDto)
        .subscribe(() => this.messageDto.content = "");
    }
  }
}
