import { Component, OnInit, Input, ErrorHandler } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { User } from 'src/app/_models/user';
import { FollowService } from 'src/app/_services/follow.service';
import { Follow } from 'src/app/_models/follow';
import { catchError, EMPTY, of, throwError } from 'rxjs';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-follow-button',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: 'follow-button.component.html',
  styleUrls: ['follow-button.component.css'],
})
export class FollowButtonComponent {
  @Input()
  currentUser: any;

  @Input()
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

  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private userService: UserService,
    private followService: FollowService
  ) {}

  getFollow(): void {
    this.followService
      .getFollow(this.currentUser.user.userId, this.profileUser.userId)
      .subscribe({
        next: (x) => {
          console.log(x);
        },
        error: (err: HttpErrorResponse) => {
          console.log(err.message);
        },
      });
  }

  followUnfollowUser(): any {
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
}
