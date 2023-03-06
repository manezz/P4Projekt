import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Post } from '../_models/post';
import { User } from '../_models/user';
import { PostService } from '../_services/post.service';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';



@Component({
  selector: 'app-profilepage',
  template: `
  <mat-sidenav mode="side" opened >
    <img class="profilepic"src="./assets/images/placeholder.png" width="100" height="100">
    <p>{{this.user.userName}}</p>
    ___________
    <!-- Skal være links der ændrer hvilke posts der vises (mellem alle brugerens posts / alle de post brugeren har liket) -->
    <p> likes </p>
    <p> chat </p>
  </mat-sidenav>

  <div id="post" *ngFor="let post of posts"  [routerLink]="['/post-details', post.postId]">
    <div id="user"> 
      <img class="profilepic"src="./assets/images/placeholder.png" width="50" height="50">
      <h5>{{user.userName}}</h5>
    </div>
    <h1 id="title">{{post.title}}</h1>
    <h3 id="description">{{post.desc}}</h3>
    <p id="date">Date posted: {{post.date | date:'MMM d yyyy, HH:mm a'}} </p> 
    <button class="postBtn" id="like"><3</button>
  </div>
   <p id="nomore">This user ran out of posts :(<p>
  `,
  styleUrls: ["../_css/poststyle.css"]
})
export class OtherUserProfilePageComponent implements OnInit{

  posts: Post[] = [];
  clicked: any;
  user: User = {userId: 0, userName: ''}
  
  constructor(
    private route: ActivatedRoute, 
    private postService: PostService,
    private userService: UserService,
  ){ }

  ngOnInit(): void {
    console.log(this.route.params)
    this.route.params.subscribe(params => { this.userService.getUser(params['userId']).subscribe(x => this.user = x) })
    this.route.params.subscribe(params => { this.postService.GetPostByUserId((params['userId'])).subscribe(x=> this.posts = x) })
    
    
  }

}