import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Post } from '../_models/post';
import { PostService } from '../_services/post.service';


@Component({
  selector: 'app-profilepage',
  template: `

  <div id="post" *ngFor="let data of posts">
    <h1 id="title">{{data.title}}</h1>
    <h5 id="username">{{data.user.firstName}} {{data.user.lastName}}</h5>
    <h3 id="description">{{data.desc}}</h3>
    <p id="date">{{data.date}}</p>
    <!-- <p id="tags">{{data.tag}} </p> -->
    <button class="postBtn" id="like"><3</button>
  </div>
  `,
  styleUrls: ["../_css/poststyle.css"]
})
export class ProfilepageComponent {

  posts: Post[] = [];
  
  // sætter values i getTempData til data
  constructor(private post:PostService){ }

  ngOnInit(): void {
    this.post.getAllSelf(1).subscribe((x) => (this.posts = x));
  }
}
