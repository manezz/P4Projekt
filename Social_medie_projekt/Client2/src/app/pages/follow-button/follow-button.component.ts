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

  constructor(private followService: FollowService) {}

  followed(): string {
    if (this.currentUser.user.userId === this.profileUser.followUserId) {
      return 'UnFollow';
    } else {
      return 'Follow';
    }
  }

  followUnfollowUser(): any {
    if (this.currentUser.user.userId === this.profileUser.followUserId) {
      this.unFollowUser();
    } else {
      this.followUser();
    }
  }

  followUser(): void {
    this.follow = {
      userId: this.profileUser.userId!,
      followingId: this.currentUser.user.userId,
    };
    this.followService.follow(this.follow).subscribe({
      error: (err) => {
        console.warn(Object.values(err.error.errors).join(', '));
      },
    });
    this.profileUser.followUserId = this.currentUser.user.userId;
  }

  unFollowUser(): void {
    this.followService
      .unFollow(this.profileUser.userId!, this.currentUser.user.userId)
      .subscribe({
        error: (err) => {
          console.warn(Object.values(err.error.errors).join(', '));
        },
      });
    this.profileUser.followUserId = null!;
  }
}
