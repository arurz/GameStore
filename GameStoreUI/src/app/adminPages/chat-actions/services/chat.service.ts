import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as SignalR from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subject } from 'rxjs';
import { CurrentUserDataService } from '../../../users/current-user-data/current-user-data.service';
import { TokenData } from '../../../users/current-user-data/models/token-data.enum';
import { ChatUserDto } from '../models/chat-user-dto.model';
import { Message } from '../models/message';
import { MessageDto } from '../models/message-dto.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  readonly url = "api/chat"
  serverUrl: string;

  selectedClientId = new Subject<number>();
  currentUserId: number;

  private connection: any = new SignalR.HubConnectionBuilder()
      .withUrl("https://gamestore-api-westeu.azurewebsites.net/api/chatHub")
      .build();

  private sharedObj = new Subject<Message>();

  constructor(private httpClient: HttpClient,
    private toastr: ToastrService,
    private currentUserDataService: CurrentUserDataService) {
      this.serverUrl = environment.baseURL;

    this.connection.onclose(async () => {
      await this.startConnection();
    });
    this.connection.on("Receive", (message: Message) => { 
      if(this.currentUserId !== message.fromUserId && 
        (message.toUserId === this.currentUserId || !message.toUserId)){
        this.toastr.success("You have one new message from " + message.fromUser.username);
      }
      this.mapReceivedMessage(message);
    });
    this.startConnection();  
  }

  public async startConnection() {
    try {
      await this.connection.start();
      console.log("connected");
      this.currentUserId = Number(this.currentUserDataService.getTokenData(TokenData.UserId));
    } catch (err) {
        setTimeout(() => this.startConnection(), 5000);
    } 
  }

  private mapReceivedMessage(message: Message): void {
    this.sharedObj.next(message);
  }

  public openUserChat(userId: number){
    this.selectedClientId.next(userId);
  }

  public getUserChat(): Observable<number>{
    return this.selectedClientId.asObservable();
  }

  public retrieveMappedObject(): Observable<Message> {
    return this.sharedObj.asObservable();
  }

  public broadcastMessage(msgDto: MessageDto): Observable<Message> {
    return this.httpClient.post<Message>(`${this.serverUrl}/${this.url}`, msgDto);
  }

  public getAllUsersWithChat(): Observable<ChatUserDto[]> {
    return this.httpClient.get<ChatUserDto[]>(`${this.serverUrl}/${this.url}` + "/all");
  }

  public getChatMessages(id: number): Observable<Message[]>{
    return this.httpClient.get<Message[]>(`${this.serverUrl}/${this.url}` + "/messages/" + id);
  }
}
