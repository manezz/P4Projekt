import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import Talk from 'talkjs';
// import { User } from 'talkjs/all';
import { User } from '../_models/user';
import { Deferred } from "../_helpers/deferred";

@Injectable({
  providedIn: 'root'
})
export class TalkService {
  private static APP_ID = 'tm826eLO';
  private currentTalkUser: Talk.User
  private currentSessionDeferred = new Deferred();

  constructor(private auth: AuthService) { }

  async createCurrentSession() {
    await Talk.ready;
 
    // henter useren ned der er logget på
    const currentUser = await this.auth.getUser();

    // sætter useren til talk-useren gennem create funktionen
    const currentTalkUser = await this.createTalkUser(currentUser);
    const session = new Talk.Session({
      appId: TalkService.APP_ID,
      me: currentTalkUser
    });

    this.currentTalkUser = currentTalkUser;
    this.currentSessionDeferred.resolve(session);
  }
  
  async createTalkUser(applicationUser: any) : Promise<any> {
    await Talk.ready;

    return new Talk.User({
      id: applicationUser.userId,
      name: applicationUser.userName,
    });
  }

  async createPopup(otherApplicationUser: User, keepOpen: boolean) : Promise<any> {
    const session: any = await this.currentSessionDeferred.promise;
    const conversationBuilder = await this.getOrCreateConversation(session, otherApplicationUser);
    const popup = session.createPopup(conversationBuilder, { keepOpen: keepOpen });

    return popup;
  }

  private async getOrCreateConversation(session: Talk.Session, otherApplicationUser: User) {
    const otherTalkUser = await this.createTalkUser(otherApplicationUser);
    const conversationBuilder = session.getOrCreateConversation(Talk.oneOnOneId(this.currentTalkUser, otherTalkUser));

    conversationBuilder.setParticipant(this.currentTalkUser);
    conversationBuilder.setParticipant(otherTalkUser);

    return conversationBuilder;
  }
  

}