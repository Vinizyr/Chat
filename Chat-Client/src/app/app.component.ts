import { Component, OnDestroy, OnInit } from '@angular/core';
import { SignalrService } from './signalr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy{
  title = 'Chat-Client';

  public user: string = '';
  public message: string = '';
  public messages: { user: string, message: string }[] = [];
  totalUsers: number = 0;

  constructor(
    private signalrService: SignalrService
  ){}
  
  
  ngOnInit(): void {
    this.signalrService.startConnection();
    this.signalrService.addReceiveMessageListener();
    
    this.signalrService.hubConnection.on('ReceiveMessage', (user, message) => {
      this.messages.push({ user, message });
    });

    this.signalrService.addStatisticsListener((totalUsers: number) => {
      this.totalUsers = totalUsers;
    });
  }

  public sendMessage(): void {
    if (this.user && this.message) {
      this.signalrService.sendMessage(this.user, this.message);
      this.message = '';
    }
  }

  ngOnDestroy(): void {
    this.signalrService.hubConnection.off("AskServerResponse")
  }
}
