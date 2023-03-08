import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { PostService } from '../_services/post.service';
import { Post } from '../_models/post';
import { Tag } from '../_models/tags';


@Component({
  selector: 'app-indexpage',
  template: `    
  <app-createPostpage></app-createPostpage>
  
  <div id="post" *ngFor="let post of posts" [routerLink]="['/post-details', post.postId]">
    <div id="user" (click)="postLink(this.post.user)"> 
      <img id="profilepic"src="./assets/images/placeholder.png" width="50" height="50">
      <h5 id="userName">{{post.user?.userName}}</h5>
    </div>
    <div id="content">
      <h1 id="title">{{post.title}}</h1>
      <h3 id="description">{{post.desc}}</h3>
      <p id="tags" *ngFor="let tag of post.tags" style="display: inline;">#{{ tag.name }}, &#160;</p>
      
      <p id="date">{{post.date | date:'MMM d yyyy, HH:mm a'}}</p> 
    </div>
    <button class="postBtn" id="like"><3</button>
    <button class="editBtn" id="edit" *ngIf="this.currentUser.loginResponse.user.userId == this.post.user?.userId" [routerLink]="['/editPost', post.postId]">⛭</button>
  </div>
  
  `,
  styleUrls: ["../_css/poststyle.css"]
  
})
export class IndexpageComponent {
  currentUser: any = {}
  posts: Post[] = [];
  tags: Tag[] = []

  constructor(
    private postService: PostService,
    private authService: AuthService,
    private router: Router
  )
  { }

  ngOnInit(): void {
    this.authService.currentUser.subscribe(x => this.currentUser = x )
    this.postService.getAll().subscribe(p => this.posts = p)
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