import { Injectable } from '@angular/core';
import { Message } from './message';
import { Http, Headers } from '@angular/http';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class MessageService {

  private messagesUrl = 'http://mathackwebapitemplate20170512092055.azurewebsites.net/api/Messages';
  private headers = new Headers({'Content-Type': 'application/json'});
  
  constructor(private http: Http) { }

  getMessages(): Promise<Message[]>{
    return this.http.get(this.messagesUrl)
      .toPromise()
      .then(response => response.json() as Message[])
      .catch(this.handleError);
  }

  postMessages(message: Message): Promise<Message> {
    return this.http.post(this.messagesUrl, JSON.stringify(message),{headers: this.headers})
      .toPromise()
      .then(res => res.json() as Message)
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error); // for demo purposes only
    return Promise.reject(error.message || error);
  }
}
