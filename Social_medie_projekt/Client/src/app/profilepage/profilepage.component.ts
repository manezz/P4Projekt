import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Post } from '../_models/post';
import { PostService } from '../_services/post.service';
import { User } from '../_models/user';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';



@Component({
  selector: 'app-profilepage',
  template: `
    <mat-sidenav mode="side" opened >
        <img class="profilepic"src="./assets/images/placeholder.png" width="100" height="100">
        <p>{{this.currentUser.loginResponse.user.firstName}} {{this.currentUser.loginResponse.user.lastName}}</p>
    </mat-sidenav>

  <div id="post" *ngFor="let posts of this.currentUser.loginResponse.user.posts"  [routerLink]="['/post-details', posts.postId]">
    <h5 id="username"> <img class="profilepic"src="./assets/images/placeholder.png" width="50" height="50">
    {{this.currentUser.loginResponse.user.firstName}} {{this.currentUser.loginResponse.user.lastName}}
    </h5>
    <h1 id="title">{{posts.title}}</h1>
    <h3 id="description">{{posts.desc}}</h3>
    <p id="date">Date posted: {{posts.date | date:'MMM d yyyy, HH:mm a'}} </p> 
    <button class="postBtn" id="like"><3</button>
   </div>

   <p id="nomore">This user has no more posts :(<p>
    
  `,
  styleUrls: ["../_css/poststyle.css"]
})
export class ProfilepageComponent implements OnInit{

  currentUser: any = {};
  
   user: User = {
    userId: 0,
    firstName: '',
    lastName: '',
    address: '',
    created: new Date,
    posts: [],
    login: {loginId: 0, email: '', password: ''}
  } 

  
  // sætter values i getTempData til data
  constructor(private userService:UserService, private route: ActivatedRoute, private authService: AuthService){ }


  ngOnInit(): void {
 

   this.authService.currentUser.subscribe(x => {
    this.currentUser = x;
  })

   console.log(this.currentUser.loginResponse)
   console.log(this.currentUser.loginResponse.user)

  }

  

}
