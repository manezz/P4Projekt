import { Component } from '@angular/core';
import { AuthService } from '../../_services/auth.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { PostService } from '../../_services/post.service';
import { Post } from '../../_models/post';
import { CommonModule } from '@angular/common';
import { LikeComponent } from '../like/like.component';

@Component({
  selector: 'app-post-details',
  standalone: true,
  imports: [CommonModule, RouterLink, LikeComponent],
  templateUrl: 'post-detail.component.html',
  styleUrls: ['post-detail.component.css'],
})
export class PostDetailsComponent {
  currentUser: any = {};

  post: Post = {
    postId: 0,
    userId: 0,
    title: '',
    desc: '',
    likeUserId: 0,
    tags: [
      {
        tagId: 0,
        name: '',
      },
    ],
    postLikes: {
      likes: 0,
    },
    date: new Date(),
    user: {
      userId: 0,
      userName: '',
    },
  };

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private postService: PostService
  ) {}

  ngOnInit(): void {
    this.authService.currentUser.subscribe((x) => (this.currentUser = x));
    this.route.params.subscribe((params) => {
      this.postService
        .GetPostByPostId(params['postId'])
        .subscribe((x) => (this.post = x));
    });
  }
}
