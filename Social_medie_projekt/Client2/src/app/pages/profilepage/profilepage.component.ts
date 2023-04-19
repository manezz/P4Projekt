import { Component, OnInit, HostListener } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Post } from '../../_models/post';
import { PostService } from '../../_services/post.service';
import { AuthService } from '../../_services/auth.service';
import { MatSidenavModule } from '@angular/material/sidenav';
import { CreatePostPageComponent } from '../create-postpage/create-postpage.component';
import { PostComponent } from '../post/post.component';
import { ProfilepageSidenavComponent } from '../profilepage-sidenav/profilepage-sidenav.component';
import { ProfilepageCenternavComponent } from 'src/app/pages/profilepage-centernav/profilepage-centernav.component';
import { UserService } from 'src/app/_services/user.service';
import { User } from 'src/app/_models/user';

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
  profileUser: User = {
    userName: '',
    userImage: {
      image: '',
    },
  };
  posts: Post[] = [];
  screenWidth: number = 0;

  constructor(
    private postService: PostService,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    if (this.router.url === '/profile') {
      this.authService.currentUser.subscribe({
        next: (x) => {
          this.profileUser = x.user!;
          this.getPosts();
        },
      });
    } else {
      this.route.params.subscribe((params) => {
        this.userService.getUser(params['userId']).subscribe({
          next: (x) => {
            this.profileUser = x;
            this.getPosts();
          },
        });
      });
    }

    this.screenWidth = window.innerWidth;
  }

  getPosts(): void {
    this.postService
      .GetPostByUserId(this.profileUser.userId!)
      .subscribe((x) => (this.posts = x));
  }

  @HostListener('window:resize', ['$event'])
  onResize(): void {
    this.screenWidth = window.innerWidth;
  }
}
