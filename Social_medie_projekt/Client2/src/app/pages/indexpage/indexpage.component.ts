import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { PostService } from '../../_services/post.service';
import { AuthService } from '../../_services/auth.service';
import { Post } from '../../_models/post';
import { Tag } from '../../_models/tag';
import { CreatePostPageComponent } from '../create-postpage/create-postpage.component';
import { LikeComponent } from '../like/like.component';
import { PostComponent } from '../post/post.component';

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
export class IndexpageComponent implements OnInit {
  currentUser: any = {};
  posts: Post[] = [];
  tags: Tag[] = [];

  constructor(private postService: PostService) {
    this.tags = [];
  }

  ngOnInit(): void {
    this.postService.getAll().subscribe((p) => (this.posts = p));
  }
}
