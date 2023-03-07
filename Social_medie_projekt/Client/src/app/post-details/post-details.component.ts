import { Component } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { PostService } from '../_services/post.service';
import { Post } from '../_models/post';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-post-details',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div id="post">
      <button
        class="editBtn"
        id=""
        *ngIf="post.user?.userId == this.currentUser.loginResponse.user.userId"
        [routerLink]="['/editPost', post.postId]"
      >
        ⚙
      </button>
      <img
        class="profilepic"
        src="./assets/images/placeholder.png"
        width="50"
        height="50"
      />
      <h5 id="username">{{ post.user?.userName }}</h5>
      <h1 id="title">{{ post.title }}</h1>
      <h3 id="description">{{ post.desc }}</h3>
      <!-- <p id="tags" *ngFor="let tag of tags">#{{tag.tag}}, </p> -->
      <h6 id="tags">#{{ post.tags }},</h6>
      <p id="date">{{ post.date | date : 'MMM d yyyy, HH:mm a' }}</p>
      <button class="postBtn" id="like"><3</button>
    </div>
  `,
  styleUrls: ['../_css/poststyle.css'],
})
export class PostDetailsComponent {
  currentUser: any = {};

  post: Post = {
    postId: 0,
    userId: 0,
    title: '',
    desc: '',
    tags: '',
    likes: 0,
    date: new Date(),
    user: {
      userId: 0,
      userName: '',
    },
  };

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private postService: PostService
  ) {}

  ngOnInit(): void {
    this.authService.currentUser.subscribe((x) => (this.currentUser = x));
    this.route.params.subscribe((params) => {
      this.postService
        .GetPostByPostId(params['postId'])
        .subscribe((x) => (this.post = x));
    });
  }
}
