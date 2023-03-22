import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../_services/auth.service';
import { ActivatedRoute, RouterLink, Router } from '@angular/router';
import { PostService } from '../../_services/post.service';
import { Post } from '../../_models/post';
import { CommonModule } from '@angular/common';
import { LikeComponent } from '../like/like.component';
import { PostComponent } from '../post/post.component';

@Component({
  selector: 'app-post-details',
  standalone: true,
  imports: [CommonModule, RouterLink, LikeComponent, PostComponent],
  templateUrl: 'post-detail.component.html',
})
export class PostDetailsComponent implements OnInit {
  currentUser: any = {};

  posts: Post[] = [];

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
        .subscribe((x) => this.posts.push(x));
    });
  }
}
