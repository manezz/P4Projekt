import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Post } from '../_models/post';
import { Tag } from '../_models/tags';
import { AuthService } from '../_services/auth.service';
import { AppComponent } from '../app.component';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { PostService } from '../_services/post.service';

@Component({
  selector: 'app-postpage',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: ` <div class="formControl">
    <div class="formDiv">
      <label>Title</label>
      <input type="string" [(ngModel)]="title" />
    </div>
    <div class="formDiv">
      <label>Decription</label>
      <input type="string" [(ngModel)]="desc" />
      <label>Tags</label>
      <input type="text" [(ngModel)]="tags" />
      <label>Picture</label>
      <input type="string" [(ngModel)]="pictureURL" />
      <!-- </div>
        <span class="error"*ngIf="userForm.get('userName')?.invalid && userForm.get('passHash')?.touched">Fill out form!</span>
      </div> -->
      <div class="buttonDiv">
        <button type="button" (click)="posting()">Post</button>
        <button class="right" routerLink="/main">Cancel</button>
      </div>
    </div>
  </div>`,
  styles: [
    `
      .formControl {
        display: flex;
        height: 700px;
        justify-content: center;
        align-items: center;
        flex-direction: column;
      }
    `,
  ],
})
export class PostPageComponent {
  constructor(
    private auth: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private AppComponent: AppComponent,
    private postservice: PostService
  ) {
    this.auth.currentUser.subscribe((x) => (this.currentUser = x));
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    };
  }
  userId: number = 0;
  title: string = '';
  desc: string = '';
  //pictureURL?: string = '';
  tags?: Tag[] = [];

  currentUser: any = {};

  posts: Post[] = [];

  posting() {
    let post = {
      // userId: this.auth.CurrentUserValue.loginResponse.user.userId,
      userId: this.auth.CurrentUserValue.user?.userId,
      title: this.title,
      desc: this.desc,
      //pictureURL: this.pictureURL,
      tags: this.tags,
    } as Post;
    this.postservice.createPost(post).subscribe();
    this.router.navigate(['/main']);
    this.postservice.getAll().subscribe((x) => (this.posts = x));
  }
}
