import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../_models/user';
import { TalkService } from '../_services/talk.service';
import Talk from 'talkjs';

@Component({
  selector: 'app-chat',
  template: ` <div #talkjsContainer style="height: 600px">Loading...</div> `,
  styleUrls: [],
})
export class ChatComponent {
  private chatPopup: Talk.Popup;

  constructor(
    private talkService: TalkService,
    private route: ActivatedRoute,
    private router: Router
  ) // private user: User
  {}

  ngOnInit() {
    // this.preloadChatPopup(this.user);
  }

  showChatPopup() {
    this.chatPopup.show();
  }

  private async preloadChatPopup(vendor: User) {
    this.chatPopup = await this.talkService.createPopup(vendor, false);
    this.chatPopup.mount({ show: false });
  }
}
