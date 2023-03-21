import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Post } from '../../_models/post';
import { User } from '../../_models/user';
import { PostService } from '../../_services/post.service';
import { AuthService } from '../../_services/auth.service';
import { UserService } from '../../_services/user.service';
import { MatSidenavModule } from '@angular/material/sidenav';

@Component({
  selector: 'app-profilepage2',
  standalone: true,
  imports: [CommonModule, RouterLink, MatSidenavModule],
  templateUrl: 'profilepage.component.html',
  styleUrls: ['profilepage.component.css'],
})
export class OtherUserProfilePageComponent implements OnInit {
  posts: Post[] = [];
  clicked: any;
  user: User = { userId: 0, userName: '' };
  currentUser: any = {};

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private postService: PostService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.userService
        .getUser(params['userId'])
        .subscribe((x) => (this.user = x));
    });
    this.route.params.subscribe((params) => {
      this.postService
        .GetPostByUserId(params['userId'])
        .subscribe((x) => (this.posts = x));
    });
  }

  postLink(user: any) {
    if (user.userId == this.currentUser.loginResponse.user.userId) {
      // linker til brugerens egen profilside
      this.router.navigateByUrl('/profile');
    } else {
      // linker til en andens bruger profilside
      this.router.navigate(['/profile/', user.userId]);
    }
  }
}
