import { InputModalityDetector } from '@angular/cdk/a11y';
import { Component, Input } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-like',
  standalone: true,
  template: `
    <button (click)="like()" class="postBtn {{ liked() }}" id="like"><3</button>
  `,
  styles: [
    `
      .liked {
        background-color: hotpink;
      }
      #like:hover {
        background-color: rgb(211, 211, 211);
        cursor: pointer;
      }
      /* skal konstant være ændret hvis en post er liked */
      #like:focus {
        background-color: rgb(209, 43, 43);
        color: white;
      }
      .postBtn {
        position: absolute;
        right: 20px;
        bottom: 20px;
        border: 1px solid black;
        border-radius: 10px;
      }
    `,
  ],
})
export class LikeComponent {
  @Input()
  likeUserId: any;

  currentUser: any;

  constructor(private auth: AuthService) {}

  like = (): any => {
    console.log('test');
  };

  liked = (): string => {
    let className = '';

    if (this.likeUserId === this.auth.CurrentUserValue.loginId) {
      className = 'liked';
    }
    return className;
  };
}
