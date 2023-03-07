import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Data, RouterModule } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { PostService } from '../_services/post.service';
import { Post } from '../_models/post';
import { Login } from '../_models/login';
import { Tag } from '../_models/tags';
import { MatSidenavModule } from '@angular/material/sidenav';
import { CreatePostPageComponent } from '../profilepage/createPostpage.component';

@Component({
  selector: 'app-indexpage',
  standalone: true,
  imports: [CommonModule, RouterModule, CreatePostPageComponent],
  template: `
    <app-createPostpage></app-createPostpage>

    <!-- looper igennem alle post fra data(DataService) -->
    <div
      id="post"
      *ngFor="let post of posts"
      [routerLink]="['/post-details', post.postId]"
    >
      <h5 id="username">
        <img
          class="profilepic"
          src="./assets/images/placeholder.png"
          width="50"
          height="50"
        />
        {{ post.user?.userName }}
      </h5>
      <h1 id="title">{{ post.title }}</h1>
      <h3 id="description">{{ post.desc }}</h3>
      <!-- <p id="tags" *ngFor="let tag of tags">#{{tag.tag}}, </p> -->
      <p id="tags" *ngIf="post.tags">#{{ post.tags }},</p>
      <p id="date">{{ post.date | date : 'MMM d yyyy, HH:mm a' }}</p>
      <button class="postBtn" id="like"><3</button>
    </div>
  `,
  styleUrls: ['../_css/poststyle.css'],
})
export class IndexpageComponent {
  posts: Post[] = [];
  // post: Post = {
  //   postId: 0,
  //   title: '',
  //   desc: '',
  //   tags: '',
  //   likes: 0,
  //   date: new Date,
  //   user: {
  //     userId: 0,
  //     userName: '',
  //     created: new Date,
  //     login: {
  //       loginId: 1,
  //       email: '',
  //       password: ''
  //     },
  //   posts: []}
  // }

  tags: Tag[] = [];
  tag: Tag = { tagId: 0, tag: '' };

  constructor(private postService: PostService, private auth: AuthService) {}

  ngOnInit(): void {
    this.postService.getAll().subscribe((p) => (this.posts = p));
  }
}
