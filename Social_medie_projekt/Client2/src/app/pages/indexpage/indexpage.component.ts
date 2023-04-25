import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { PostService } from '../../_services/post.service';
import { AuthService } from '../../_services/auth.service';
import { Post } from '../../_models/post';
import { Tag } from '../../_models/tag';
import { CreatePostPageComponent } from '../create-postpage/create-postpage.component';
import { LikeComponent } from '../like/like.component';
import { PostComponent } from '../post/post.component';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-indexpage',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    CreatePostPageComponent,
    LikeComponent,
    PostComponent,
  ],
  templateUrl: 'indexpage.component.html',
})
export class IndexpageComponent implements OnInit, OnDestroy {
  currentUser: any = {};
  posts: Post[] = [];
  tags: Tag[] = [];

  private subscription = Subscription.EMPTY;

  constructor(private postService: PostService) {
    this.tags = [];
  }

  ngOnInit(): void {
    this.subscription = this.postService
      .getAll()
      .subscribe((p) => (this.posts = p));
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
