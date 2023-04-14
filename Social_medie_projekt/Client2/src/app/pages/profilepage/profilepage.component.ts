import { Component, OnInit, HostListener } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Post } from '../../_models/post';
import { PostService } from '../../_services/post.service';
import { AuthService } from '../../_services/auth.service';
import { MatSidenavModule } from '@angular/material/sidenav';
import { CreatePostPageComponent } from '../create-postpage/create-postpage.component';
import { PostComponent } from '../post/post.component';
import { ProfilepageSidenavComponent } from '../profilepage-sidenav/profilepage-sidenav.component';
import { ProfilepageCenternavComponent } from 'src/app/profilepage-centernav/profilepage-centernav.component';

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
  currentUser: any = {};
  posts: Post[] = [];
  screenWidth: number = 0;

  constructor(
    private postService: PostService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.authService.currentUser.subscribe((x) => (this.currentUser = x));
    this.postService
      .GetPostByUserId(this.currentUser.user.userId)
      .subscribe((x) => (this.posts = x));

    this.screenWidth = window.innerWidth;
  }

  @HostListener('window:resize', ['$event'])
  onResize(): void {
    this.screenWidth = window.innerWidth;
  }
}
