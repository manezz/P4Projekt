import { Component, OnInit, HostListener } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Post } from '../../_models/post';
import { PostService } from '../../_services/post.service';
import { AuthService } from '../../_services/auth.service';
import { MatSidenavModule } from '@angular/material/sidenav';
import { CreatePostPageComponent } from '../create-postpage/create-postpage.component';
import { PostComponent } from '../post/post.component';
import { ProfilepageSidenavComponent } from '../profilepage-sidenav/profilepage-sidenav.component';
import { ProfilepageCenternavComponent } from 'src/app/pages/profilepage-centernav/profilepage-centernav.component';
import { UserService } from 'src/app/_services/user.service';
import { User } from 'src/app/_models/user';
import { FollowService } from 'src/app/_services/follow.service';
import { Follow } from 'src/app/_models/follow';

@Component({
  selector: 'app-profilepage',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatSidenavModule,
    CreatePostPageComponent,
    PostComponent,
    ProfilepageSidenavComponent,
    ProfilepageCenternavComponent,
  ],
  templateUrl: 'profilepage.component.html',
})
export class ProfilepageComponent implements OnInit {
  isCurrentUser: boolean = false;
  currentUser: any;
  profileUser: User = {
    userName: '',
    userImage: {
      image: '',
    },
  };
  follow: Follow = {
    userId: 0,
    followingId: 0,
  };
  posts: Post[] = [];
  screenWidth: number = 0;

  constructor(
    private postService: PostService,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private userService: UserService,
    private followService: FollowService
  ) {}

  ngOnInit(): void {
    if (this.router.url === '/profile') {
      this.authService.currentUser.subscribe({
        next: (x) => {
          this.profileUser = x.user!;
          this.isCurrentUser = true;

          if (this.currentUser === undefined) {
            console.log('currentUser is not undefined');
          }

          this.getPosts();
        },
      });
    } else {
      this.route.params.subscribe((params) => {
        this.userService.getUser(params['userId']).subscribe({
          next: (x) => {
            this.profileUser = x;

            if (this.currentUser === undefined) {
              console.log('currentUser is undefined');
            }

            this.authService.currentUser.subscribe(
              (x) => (this.currentUser = x)
            );
            this.getPosts();
            // this.getFollow();
          },
        });
      });
    }
    this.screenWidth = window.innerWidth;
    this.getFollow();
  }

  getPosts(): void {
    this.postService
      .GetPostByUserId(this.profileUser.userId!)
      .subscribe((x) => (this.posts = x));
  }

  getFollow(): any {
    this.followService
      .getFollow(this.currentUser.user.userId, this.profileUser.userId)
      .subscribe({
        next: (x) => {
          this.unFollow(); // if follow exists - unfollow
          document.getElementById('followBtn')!.innerHTML = 'follow'; // after unfollowed - show "follow"
        },
        error: (x) => {
          this.Follow(); // if follow doesn't exist - follow
          document.getElementById('followBtn')!.innerHTML = 'unfollow'; // after followed - show "unfollow"
        },
      });
  }

  Follow() {
    this.follow = {
      userId: this.currentUser.user.userId,
      followingId: this.profileUser.userId!,
    };
    this.followService.follow(this.follow).subscribe({
      next: (x) => {
        console.log('user followed');
      },
      error: (err) => {
        console.warn('user already followed');
      },
    });
  }

  unFollow() {
    this.followService
      .unFollow(this.currentUser.user.userId, this.profileUser.userId!)
      .subscribe({
        next: (x) => {
          console.log('user successfully unfollowed');
        },
        error: (err) => {
          console.warn('user not followed');
        },
      });
  }

  @HostListener('window:resize', ['$event'])
  onResize(): void {
    this.screenWidth = window.innerWidth;
  }
}
