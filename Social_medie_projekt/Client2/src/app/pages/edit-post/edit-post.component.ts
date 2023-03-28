import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../_services/auth.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { PostService } from '../../_services/post.service';
import { Post } from '../../_models/post';
import { Tag } from 'src/app/_models/tag';
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
export class EditPostComponent implements OnInit {
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

  ngOnInit(): void {
    this.authService.currentUser.subscribe((x) => (this.currentUser = x));
    this.route.params.subscribe((params) => {
      this.postService.GetPostByPostId(params['postId']).subscribe((x) => {
        (this.post = x), this.insertValues();
      });
    });
  }

  tagNames(): string {
    let val: string[] = [];
    this.post.tags?.forEach((e: Tag) => {
      val.push(e.name);
    });
    return val.toString();
  }

  insertValues(): void {
    this.postForm.setValue({
      Title: this.post.title,
      Content: this.post.desc,
      Tags: this.tagNames(),
    });
  }

  newTags(): Tag[] {
    let splitTags: Tag[] = [];
    this.postForm.value.Tags.split(',').forEach((e: any) => {
      splitTags.push({ name: e });
    });
    return splitTags;
  }

  edit(): void {
    if (!this.postForm.pristine) {
      this.post = {
        postId: this.post.postId,
        title: this.postForm.value.Title,
        desc: this.postForm.value.Content,
        tags: this.newTags(),
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

  delete(postId: number): void {
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
    return {
      postId: 0,
      title: '',
      desc: '',
      tags: [
        {
          tagId: 0,
          name: '',
        },
      ],
    };
  }

  resetForm(): FormGroup {
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
