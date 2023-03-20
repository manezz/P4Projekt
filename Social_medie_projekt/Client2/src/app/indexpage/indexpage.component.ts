import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router} from '@angular/router';
import { PostService } from '../_services/post.service';
import { AuthService } from '../_services/auth.service';
import { Post } from '../_models/post';
import { Tag } from '../_models/tags';
import { CreatePostPageComponent } from '../create-postpage/create-postpage.component';
import { LikeComponent } from '../components/like.component';

@Component({
  selector: 'app-indexpage',
  standalone: true,
  imports: [CommonModule, RouterModule, CreatePostPageComponent, LikeComponent],
  templateUrl: 'indexpage.component.html',
  styleUrls: ['indexpage.component.css'],
})
export class IndexpageComponent {
  currentUser: any = {}
  posts: Post[] = [];
  tags: Tag[] = [];

  constructor(
    private postService: PostService,
    private authService: AuthService,
    private router: Router
    ){
    this.tags = [];
  }

  ngOnInit(): void {
    this.authService.currentUser.subscribe(x => this.currentUser = x )
    this.postService.getAll().subscribe(p => this.posts = p)
  }

  postLink(user: any) {
    if(user.userId == this.currentUser.loginId){
      // linker til brugerens egen profilside
      this.router.navigateByUrl('/profile')
    }
    else{
      // linker til en andens bruger profilside
      this.router.navigate(['/profile/', user.userId])
    }
  }
}
