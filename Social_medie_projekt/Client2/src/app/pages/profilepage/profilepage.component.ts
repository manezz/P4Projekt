import { Component, OnInit } from '@angular/core';
import { RouterLink, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Post } from '../../_models/post';
import { PostService } from '../../_services/post.service';
import { AuthService } from '../../_services/auth.service';
import { MatSidenavModule } from '@angular/material/sidenav';
import { CreatePostPageComponent } from '../create-postpage/create-postpage.component';

@Component({
  selector: 'app-profilepage',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatSidenavModule,
    CreatePostPageComponent,
  ],
  templateUrl: 'profilepage.component.html',
  styleUrls: ['profilepage.component.css'],
})
export class ProfilepageComponent implements OnInit {
  currentUser: any = {};
  posts: Post[] = [];
  clicked: any;

  constructor(
    private postService: PostService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.currentUser.subscribe(x => this.currentUser = x)
    this.postService.GetPostByUserId(this.currentUser.user.userId).subscribe(x => this.posts = x)
  }

  postLink(user: any) {
    if(user.userId == this.currentUser.loginResponse.user.userId){
      // linker til brugerens egen profilside
      this.router.navigateByUrl('/profile')
    }
    else{
      // linker til en andens bruger profilside
      this.router.navigate(['/profile/', user.userId])
    }
  }
}
