import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Like } from '../../_models/like';
import { AuthService } from '../../_services/auth.service';
import { LikeService } from '../../_services/like.service';

@Component({
  selector: 'app-like',
  standalone: true,
  imports: [CommonModule],
  template: `
    <button (click)="likeDislikePost()" class="postBtn {{ liked() }}" id="like">
      <3
    </button>
  `,
  styles: [
    `
      .liked {
        background-color: hotpink;
      }
      #like:hover {
        background-color: rgb(211, 211, 211);
        cursor: pointer;
      }
      /* skal konstant være ændret hvis en post er liked */
      #like:focus {
        color: white;
      }
      .postBtn {
        position: absolute;
        right: 20px;
        bottom: 20px;
        border: 1px solid black;
        border-radius: 10px;
      }
    `,
  ],
})
export class LikeComponent {
  @Input()
  likeUserId: any;

  @Input()
  postPostId: any;

  currentUser: any;

  like: Like = {
    userId: 0,
    postId: 0,
  };

  constructor(private auth: AuthService, private likeService: LikeService) {}

  likeDislikePost(): void {
    if (this.likeUserId === this.auth.CurrentUserValue.user?.userId) {
      this.dislikePost();
    } else {
      this.likePost();
    }
  }

  likePost(): void {
    this.like = {
      userId: this.auth.CurrentUserValue.user?.userId!,
      postId: this.postPostId,
    };

    this.likeService.postLike(this.like).subscribe({
      error: (err) => {
        console.warn(Object.values(err.error.errors).join(', '));
      },
    });
    this.likeUserId = this.auth.CurrentUserValue.user?.userId;
  }

  dislikePost(): void {
    this.likeService
      .deleteLike(this.auth.CurrentUserValue.user?.userId!, this.postPostId)
      .subscribe({
        error: (err) => {
          console.warn(Object.values(err.error.errors).join(', '));
        },
      });
    this.likeUserId = null;
  }

  liked(): string {
    let className = '';

    if (this.likeUserId === this.auth.CurrentUserValue.user?.userId) {
      className = 'liked';
    }
    return className;
  }
}
