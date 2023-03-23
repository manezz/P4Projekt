import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Like } from '../../_models/like';
import { AuthService } from '../../_services/auth.service';
import { LikeService } from '../../_services/like.service';

@Component({
  selector: 'app-like',
  standalone: true,
  imports: [CommonModule],
  templateUrl: 'like.component.html',
  styleUrls: ['like.component.css'],
})
export class LikeComponent {
  @Input()
  post: any;

  currentUser: any;

  like: Like = {
    userId: 0,
    postId: 0,
  };

  constructor(private auth: AuthService, private likeService: LikeService) {}

  likeDislikePost(): void {
    if (this.post.likeUserId === this.auth.CurrentUserValue.user?.userId) {
      this.dislikePost();
    } else {
      this.likePost();
    }
  }

  likePost(): void {
    this.like = {
      userId: this.auth.CurrentUserValue.user?.userId!,
      postId: this.post.postId,
    };

    this.likeService.postLike(this.like).subscribe({
      error: (err) => {
        console.warn(Object.values(err.error.errors).join(', '));
      },
    });
    this.post.likeUserId = this.auth.CurrentUserValue.user?.userId;
    this.post.postLikes.likes += 1;
  }

  dislikePost(): void {
    this.likeService
      .deleteLike(this.auth.CurrentUserValue.user?.userId!, this.post.postId)
      .subscribe({
        error: (err) => {
          console.warn(Object.values(err.error.errors).join(', '));
        },
      });
    this.post.likeUserId = null;
    this.post.postLikes.likes -= 1;
  }

  liked(): string {
    let className = '';

    if (this.post.likeUserId === this.auth.CurrentUserValue.user?.userId) {
      className = 'liked';
    }
    return className;
  }
}
