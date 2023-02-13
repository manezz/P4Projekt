import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Post } from '../_models/post';
import { PostService } from '../_services/post.service';
import { AuthService } from '../_services/auth.service';



@Component({
  selector: 'app-profilepage',
  template: `
  <mat-sidenav mode="side" opened >
    <img class="profilepic"src="./assets/images/placeholder.png" width="100" height="100">
    <p>{{this.currentUser.loginResponse.user.userName}}</p>
    ___________
    <!-- Skal være links der ændrer hvilke posts der vises (mellem alle ens posts / alle de post brugeren har liket) -->
    <p> likes </p>
    <p> chat </p>
  </mat-sidenav>
  
  <app-createPostpage></app-createPostpage>

  <div id="post" *ngFor="let post of posts"  [routerLink]="['/post-details', post.postId]">
    <h5 id="username"> 
      <img class="profilepic"src="./assets/images/placeholder.png" width="50" height="50">
      {{this.currentUser.loginResponse.user.userName}}
    </h5>
    <h1 id="title">{{post.title}}</h1>
    <h3 id="description">{{post.desc}}</h3>
    <p id="date">Date posted: {{post.date | date:'MMM d yyyy, HH:mm a'}} </p> 
    <button class="postBtn" id="like"><3</button>
  </div>
   <p id="nomore">This user ran out of posts :(<p>
    
  `,
  styleUrls: ["../_css/poststyle.css"]
})
export class ProfilepageComponent implements OnInit{

  currentUser: any = {};
  posts: Post[] = [];
  clicked: any;
  
  constructor(
    private postService:PostService,
    private authService: AuthService
  ){ }

  ngOnInit(): void {
    this.authService.currentUser.subscribe(x => this.currentUser = x )
    this.postService.GetPostByUserId(this.currentUser.loginResponse.user.userId).subscribe(x=> this.posts = x)
  }

}