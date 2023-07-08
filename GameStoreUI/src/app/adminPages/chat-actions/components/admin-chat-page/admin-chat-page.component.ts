import { Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';
import { CurrentUserDataService } from '../../../../users/current-user-data/current-user-data.service';
import { TokenData } from '../../../../users/current-user-data/models/token-data.enum';
import { Message } from '../../models/message';
import { MessageDto } from '../../models/message-dto.model';
import { ChatService } from '../../services/chat.service';

@Component({
  selector: 'admin-chat-page',
  templateUrl: './admin-chat-page.component.html',
  styleUrls: ['./admin-chat-page.component.css']
})
export class AdminChatPageComponent implements OnInit {
  clientId: number;

  tempText: string = "";
  messages: Message[] = [];
  messageDto: MessageDto = new MessageDto();
  currentUserId: number;

  @ViewChild('scrollY') private scrollContainer: ElementRef;

  constructor(private chatService: ChatService,
    private currentUserDataService: CurrentUserDataService) {
  }

    @HostListener('keydown.enter')
    handleEnterButton() {
      this.sendMessage();
    }
  
    ngAfterViewChecked() {        
      this.scrollToBottom();        
    } 
  
    scrollToBottom(): void {
      this.scrollContainer.nativeElement.scrollTop = this.scrollContainer.nativeElement.scrollHeight;
    }
  
    ngOnInit(): void {
      this.currentUserId = this.currentUserDataService.getTokenData(TokenData.UserId);
      this.chatService.retrieveMappedObject()
        .subscribe((acceptedMessage: Message) => { 
          if(acceptedMessage.fromUserId == this.clientId || acceptedMessage.toUserId){
            this.messages.push(acceptedMessage); 
            this.scrollToBottom();
          }
        });

      this.chatService.getUserChat()
        .subscribe((userId) =>{
          this.clientId = userId;
          this.getMessages();
        })
    }

  getMessages(){
    this.chatService.getChatMessages(this.clientId)
      .subscribe((messages) =>{
        this.messages = messages;
      })
  }

  sendMessage(){
    if(this.messageDto.content.length > 0){
      this.messageDto.toUserId = this.clientId;
      this.messageDto.fromUserId = this.currentUserId;

      this.chatService.broadcastMessage(this.messageDto)
        .subscribe(() => this.messageDto.content = "");
    }
  }
}
