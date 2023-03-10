import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { PostService } from '../_services/post.service';
import { Post } from '../_models/post';
import { FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-editPost',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, MatInputModule],
  templateUrl: 'edit-post.component.html',
  styleUrls: ['edit-post.component.css'],
})
export class EditPostComponent {
  currentUser: any = {};
  postForm: FormGroup = this.resetForm();
  post: Post = this.resetPost();
  posts: Post[] = [];
  titleCharLenght: number | undefined; //til at vise hvor mange tegn der kan være i post-title
  contentCharLenght: number | undefined; //til at vise hvor mange tegn der kan være i post-content

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private postService: PostService,
    private router: Router
  ) {}

  ngOnInit() {
    this.authService.currentUser.subscribe((x) => (this.currentUser = x));
    this.route.params.subscribe((params) => {
      this.postService
        .GetPostByPostId(params['postId'])
        .subscribe((x) => (this.post = x));
    });

    console.log(this.post);

    this.insertValues();
  }

  insertValues() {
    (<HTMLInputElement>document.getElementById('title')).value =
      this.post.title;
    (<HTMLInputElement>document.getElementById('content')).value =
      this.post.desc;
    // (<HTMLInputElement>document.getElementById('tags')).value = this.post.tags;
  }

  edit() {
    if (!this.postForm.pristine) {
      this.post = {
        postId: this.post.postId,
        title: this.postForm.value.Title,
        desc: this.postForm.value.Content,
        tags: this.postForm.value.Tags,
      };

      this.postService.editPost(this.post).subscribe();

      // routes back to current posts post-detail
      this.router.navigate(['/post-details/', this.post.postId]);
    } else {
      this.insertValues();
      console.log('nothing changed');
      console.log(this.post);
    }
  }

  delete(postId: number) {
    if (confirm('Are you sure you want to delete this post?')) {
      this.postService.deletePost(postId).subscribe({
        next: (x) => {
          this.posts.push(x);

          // routes back to mainpage
          this.router.navigate(['/main']);
        },
        error: (err) => {
          console.warn(Object.values(err.error.errors).join(', '));
        },
      });
    }
  }

  resetPost(): Post {
    return { postId: 0, title: '', desc: '', tags: '' };
  }

  resetForm() {
    return new FormGroup({
      Title: new FormControl(''),
      Content: new FormControl(''),
      Tags: new FormControl(''),
    });
  }

  titleMaxLenght(event: any) {
    this.titleCharLenght = 100 - event.target.textLength;
    if (this.titleCharLenght <= 20)
      document.getElementById('titleCharLenght')!.style.color = 'red';
    else document.getElementById('titleCharLenght')!.style.color = 'black';
  }

  contentMaxLenght(event: any) {
    this.contentCharLenght = 1000 - event.target.textLength;
    if (this.contentCharLenght <= 100)
      document.getElementById('contentCharLenght')!.style.display = 'inline';
    else document.getElementById('contentCharLenght')!.style.display = 'none';
  }
}
