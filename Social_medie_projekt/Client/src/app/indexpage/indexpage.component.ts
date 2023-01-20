import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Data } from '@angular/router';
import { DataService } from '../_services/tempData.service';
import { AuthService } from '../_services/auth.service';
import { PostService } from '../_services/post.service';
import { Post } from '../_models/post';


@Component({
  selector: 'app-indexpage',
  template: `    
  <!-- looper igennem alle post fra data(DataService) -->
  <div id="post" *ngFor="let post of posts">
    <h1 id="title">{{post.title}}</h1>
    <h5 id="username">{{post.user.firstName}} {{post.user.lastName}}</h5>
    <h3 id="description">{{post.desc}}</h3>
    <p id="date">{{post.date}}</p>
    <!-- <p id="tags">{{post.tag.tag}} </p> -->
    <button class="postBtn" id="like"><3</button>
  </div>


  
  `,
  styleUrls: ["../_css/poststyle.css"]
  
})
export class IndexpageComponent {

  posts: Post[] = [];

  constructor(
    private postService: PostService,
    private auth: AuthService){
    
  }

  ngOnInit(): void {
    this.postService.getAll().subscribe((x) => (this.posts = x));
  }

}
