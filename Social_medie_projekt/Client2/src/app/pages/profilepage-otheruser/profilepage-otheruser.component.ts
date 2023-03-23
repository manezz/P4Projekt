import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Post } from '../../_models/post';
import { User } from '../../_models/user';
import { Follow } from '../../_models/follow';
import { AuthService } from '../../_services/auth.service';
import { PostService } from '../../_services/post.service';
import { FollowService } from '../../_services/follow.service';
import { UserService } from '../../_services/user.service';
import { MatSidenavModule } from '@angular/material/sidenav';
import { PostComponent } from '../post/post.component';

@Component({
  selector: 'app-profilepage2',
  standalone: true,
  imports: [CommonModule, RouterLink, MatSidenavModule, PostComponent],
  templateUrl: 'profilepage-otheruser.component.html',
  styleUrls: ['profilepage-otheruser.component.css'],
})
export class OtherUserProfilePageComponent implements OnInit {
  posts: Post[] = []
  clicked: any
  user: User = { userId: 0, userName: '' }
  currentUser: any = {}
  follow: Follow = { userId: 0, followingId: 0 }

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private postService: PostService,
    private userService: UserService,
    private followService: FollowService
  ) {
    
  }

  ngOnInit(): void {
    this.authService.currentUser.subscribe(x => this.currentUser = x)
    
    this.route.params.subscribe(params => {
      this.userService
      .getUser(params['userId'])
      .subscribe(x => this.user = x)
    })
    this.route.params.subscribe(params => {
      this.postService
      .GetPostByUserId(params['userId'])
      .subscribe(x => this.posts = x)
    })
    
  }
  
  getFollow(){
    this.followService.getFollow(this.currentUser.user.userId, this.user.userId).subscribe(x => this.follow = x)
    
    console.log(this.follow)    
  }

  Follow(){
    this.follow = { userId: this.currentUser.user.userId, followingId: this.user.userId! }
    this.followService.follow(this.follow).subscribe({
      next: x => {
        console.log("user followed")
      },
      error: (err) => {
        console.warn("user already followed")
      },
    })
  }

  unFollow(){
    this.followService.unFollow(this.currentUser.user.userId, this.user.userId!).subscribe({
      next: x => {
        console.log("user successfully unfollowed")
      },
      error: (err) => {
        console.warn("user not followed")
      },
    })
  }


}
