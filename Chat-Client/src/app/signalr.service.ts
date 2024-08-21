import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/chatHub')
      .build();
  }

  hubConnection: signalR.HubConnection;

  startConnection = () => {
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public addReceiveMessageListener(): void {
    this.hubConnection.on('ReceiveMessage', (user, message) => {
      console.log(`Message received from ${user}: ${message}`);
    });
  }

  public sendMessage(user: string, message: string): void {
    this.hubConnection.invoke('SendMessage', user, message)
      .catch(err => console.error('Error while sending message: ' + err));
  }

  public addStatisticsListener(callback: (totalUsers: number) => void): void {
    this.hubConnection.on('ReceiveStatisticsUpdate', (totalUsers) => {
      console.log(`Total users updated: ${totalUsers}`);
      callback(totalUsers);
    });
  }
}
