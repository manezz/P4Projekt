import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { PostService } from '../_services/post.service';
import { AuthService } from '../_services/auth.service';
import { Post } from '../_models/post';
import { Tag } from '../_models/tags';
import { CreatePostPageComponent } from '../create-postpage/create-postpage.component';

@Component({
  selector: 'app-indexpage',
  standalone: true,
  imports: [CommonModule, RouterModule, CreatePostPageComponent],
  templateUrl: 'indexpage.component.html',
  styleUrls: ['indexpage.component.css'],
})
export class IndexpageComponent {
  posts: Post[] = [];

  tags: Tag[] = [];

  constructor(private postService: PostService, private auth: AuthService) {
    this.tags = [];
  }

  ngOnInit(): void {
    this.postService.getAll().subscribe((p) => (this.posts = p));
  }
}
