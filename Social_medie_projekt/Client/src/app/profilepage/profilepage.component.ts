import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
    <div id="user" (click)="postLink(this.post.user)"> 
      <img id="profilepic"src="./assets/images/placeholder.png" width="50" height="50">
      <h5 id="userName">{{post.user?.userName}}</h5>
    </div>
    <div id="content">
      <h1 id="title">{{post.title}}</h1>
      <h3 id="description">{{post.desc}}</h3>
      <p id="tags" *ngFor="let tag of post.tags">#{{ tag.name | json }}, </p>
      <p id="date">{{post.date | date:'MMM d yyyy, HH:mm a'}}</p> 
    </div>
    <button class="postBtn" id="like"><3</button>
    <button class="editBtn" id="edit" *ngIf="this.currentUser.loginResponse.user.userId == this.post.user?.userId" [routerLink]="['/editPost', post.postId]">⛭</button>
  </div>
  <p id="nomore">This user ran out of posts :(<p>
    
  `,
  styleUrls: ["../_css/poststyle.css"],
  styles: [`
  #profilepic:hover{
    box-shadow: none;
  }
  `]
})
export class ProfilepageComponent implements OnInit{

  currentUser: any = {};
  posts: Post[] = [];
  clicked: any;
  
  constructor(
    private postService:PostService,
    private authService: AuthService,
    private router: Router
  ){ }

  ngOnInit(): void {
    this.authService.currentUser.subscribe(x => this.currentUser = x )
    this.postService.GetPostByUserId(this.currentUser.loginResponse.user.userId).subscribe(x=> this.posts = x)
  }

  postLink(user: any) {
    if(user.userId == this.currentUser.loginResponse.user.userId){
      // linker til brugerens egen profilside
      this.router.navigateByUrl('/profile')
    }
    else{
      // linker til en andens bruger profilside
      this.router.navigate(['/profile/', user.userId])
    }
  }

}