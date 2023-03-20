import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Post } from '../_models/post';
import { User } from '../_models/user';
import { PostService } from '../_services/post.service';
import { UserService } from '../_services/user.service';
import { MatSidenavModule } from '@angular/material/sidenav';
import { LikeComponent } from '../components/like.component';

@Component({
  selector: 'app-profilepage2',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatSidenavModule,
    LikeComponent
  ],
  templateUrl: 'profilepage.component.html',
  styleUrls: ['profilepage.component.css'],
})
export class OtherUserProfilePageComponent implements OnInit{

  posts: Post[] = [];
  user: User = {userId: 0, userName: ''}
  
  constructor(
    private route: ActivatedRoute,
    private postService: PostService,
    private userService: UserService,
  ){ }

  ngOnInit(): void {
    this.route.params.subscribe(params => { this.userService.getUser(params['userId']).subscribe(x => this.user = x) })
    this.route.params.subscribe(params => { this.postService.GetPostByUserId((params['userId'])).subscribe(x=> this.posts = x) })
  }
}
