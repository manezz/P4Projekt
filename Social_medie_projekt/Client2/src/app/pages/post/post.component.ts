import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../_services/auth.service';
import { Post } from '../../_models/post';
import { Tag } from '../../_models/tag';
import { LikeComponent } from '../like/like.component';

@Component({
  selector: 'app-post',
  standalone: true,
  imports: [CommonModule, RouterModule, LikeComponent],
  templateUrl: 'post.component.html',
  styleUrls: ['post.component.css'],
})
export class PostComponent {
  @Input()
  posts: Post[] = [];

  currentUser: any = {};
  tags: Tag[] = [];

  constructor(private authService: AuthService, private router: Router) {
    this.tags = [];
  }

  ngOnInit(): void {
    this.authService.currentUser.subscribe((x) => (this.currentUser = x));
  }

  postLink(user: any) {
    if (user.userId === this.authService.CurrentUserValue.user?.userId) {
      // linker til brugerens egen profilside
      this.router.navigateByUrl('/profile');
    } else {
      // linker til en andens bruger profilside
      this.router.navigate(['/profile/', user.userId]);
    }
  }
}
