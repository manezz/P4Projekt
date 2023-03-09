import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-indexpage',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: 'indexpage.component.html',
  styleUrls: ['indexpage.component.css'],
})
export class IndexpageComponent {
  // posts: Post[] = [];

  // tags: Tag[] = [];
  // tag: Tag = { tagId: 0, tag: '' };

  constructor() {}
  // constructor(private postService: PostService, private auth: AuthService) {}

  ngOnInit(): void {
    // this.postService.getAll().subscribe((p) => (this.posts = p));
  }
}
