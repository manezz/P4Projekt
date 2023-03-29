import { Component, OnInit } from '@angular/core';
import { RouterLink, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Post } from '../../_models/post';
import { PostService } from '../../_services/post.service';
import { AuthService } from '../../_services/auth.service';
import { MatSidenavModule } from '@angular/material/sidenav';
import { CreatePostPageComponent } from '../create-postpage/create-postpage.component';
import { PostComponent } from '../post/post.component';
import { ImageUploadComponent } from '../image-upload/image-upload.component';

@Component({
  selector: 'app-profilepage',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatSidenavModule,
    CreatePostPageComponent,
    PostComponent,
    ImageUploadComponent,
  ],
  templateUrl: 'profilepage.component.html',
  styleUrls: ['profilepage.component.css'],
})
export class ProfilepageComponent implements OnInit {
  currentUser: any = {};
  posts: Post[] = [];

  constructor(
    private postService: PostService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.authService.currentUser.subscribe((x) => (this.currentUser = x));
    this.postService
      .GetPostByUserId(this.currentUser.user.userId)
      .subscribe((x) => (this.posts = x));
  }
}
