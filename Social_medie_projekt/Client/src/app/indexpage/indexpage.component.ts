import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { PostService } from '../_services/post.service';
import { Post } from '../_models/post';
import { Login } from '../_models/login';
import { Tag } from '../_models/tags';
import { OtherUserProfilePageComponent } from '../profilepage/otherUserProfilePage.component';


@Component({
  selector: 'app-indexpage',
  template: `    
  <app-createPostpage></app-createPostpage>
  
  <!-- looper igennem alle post fra data(DataService) -->
  <div id="post" *ngFor="let post of posts" [routerLink]="['/post-details', post.postId]">
    <div id="user" (click)="postLink(this.post.user)"> 
      <img class="profilepic"src="./assets/images/placeholder.png" width="50" height="50">
      <h5>{{post.user?.userName}}</h5>
    </div>
    <h1 id="title">{{post.title}}</h1>
    <h3 id="description">{{post.desc}}</h3>
    <!-- <p id="tags" *ngFor="let tag of tags">#{{tag.tag}}, </p> -->
    <p id="tags" *ngIf="post.tags">#{{post.tags}}, </p>
    <p id="date">{{post.date | date:'MMM d yyyy, HH:mm a'}}</p> 
    <button class="postBtn" id="like"><3</button>
  </div>
  
  `,
  styleUrls: ["../_css/poststyle.css"]
  
})
export class IndexpageComponent {
  currentUser: any = {}
  posts: Post[] = [];

  tags: Tag[] = []
  tag: Tag = { tagId: 0, tag: '' }



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
      // skal også sende userId med for at finde useren
      this.router.navigate(['/profile/', user.userId])
    }
  }

}
