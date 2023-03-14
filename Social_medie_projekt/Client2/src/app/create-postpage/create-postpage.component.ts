import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../app.component';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Post } from '../_models/post';
import { PostService } from '../_services/post.service';
import { AuthService } from '../_services/auth.service';
import { FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-create-postpage',
  standalone: true,
  imports: [CommonModule, RouterLink, ReactiveFormsModule, MatInputModule],
  templateUrl: 'create-postpage.component.html',
  styleUrls: ['create-postpage.component.css'],
})
export class CreatePostPageComponent implements OnInit {
  constructor(
    private auth: AuthService,
    private postService: PostService,
    private router: Router,
    private route: ActivatedRoute,
    private AppComponent: AppComponent
  ) {}

  error: string | undefined;
  currentUser?: any = {};
  currentUserId?: number;
  post: Post = this.resetPost();
  posts: Post[] = [];
  postForm: FormGroup = this.resetForm();
  titleCharLenght: number | undefined; //til at vise hvor mange tegn der kan være i post-title
  contentCharLenght: number | undefined; //til at vise hvor mange tegn der kan være i post-content

  ngOnInit(): void {
    this.resetForm();
    this.resetPost();
    this.titleCharLenght = 100;
    this.auth.currentUser.subscribe((x) => {
      this.currentUser = x;
    });
    this.currentUserId = this.auth.CurrentUserValue.user?.userId;
    this.collapseDiv();
  }

  create() {
    this.post = {
      userId: this.currentUserId,
      postId: 0,
      title: this.postForm.value.Title,
      desc: this.postForm.value.Content,
      tags: this.postForm.value.Tags,
    };

    this.postService.createPost(this.post).subscribe({
      next: (x) => {
        this.posts.push(x);
      },
      error: (err) => {
        console.warn(Object.values(err.error.errors).join(', '));
      },
    });
  }

  cancel() {
    this.postForm = this.resetForm();
    this.post = this.resetPost();
    this.titleCharLenght = 100;
    this.contentCharLenght = 1000;
    // this.collapseDiv()
  }

  resetPost(): Post {
    // return{ userId: this.currentUserId, postId: 0, title: '', desc: '' }
    return {
      postId: 0,
      title: '',
      desc: '',
      tags: [{
        tagId: 0,
        name: '',
      }],
      date: new Date(),
      user: {
        userId: this.currentUserId,
        userName: this.auth.CurrentUserValue.user?.userName,
        created: this.auth.CurrentUserValue.user?.created,
      },
    };
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

  expandDiv() {
    document.getElementById('form')!.style.backgroundColor = 'lightgray';

    document.getElementById('title')!.style.display = 'inline';
    document.getElementById('titleCharLenght')!.style.display = 'inline';
    document.getElementById('content')!.style.display = 'inline';
    document.getElementById('contentCharLenght')!.style.display = 'inline';
    document.getElementById('tags')!.style.display = 'inline';

    document.getElementById('createBtn')!.style.display = 'inline';
    document.getElementById('collapseBtn')!.style.display = 'inline';
    document.getElementById('cancelBtn')!.style.display = 'inline';
    document.getElementById('expandBtn')!.style.display = 'none';
  }

  collapseDiv() {
    document.getElementById('form')!.style.backgroundColor = 'transparent';
    document.getElementById('form')!.style.borderTop = '3px solid lightgray';
    document.getElementById('form')!.style.borderBottom = '3px solid lightgray';

    document.getElementById('title')!.style.display = 'none';
    document.getElementById('titleCharLenght')!.style.display = 'none';
    document.getElementById('content')!.style.display = 'none';
    document.getElementById('contentCharLenght')!.style.display = 'none';
    document.getElementById('tags')!.style.display = 'none';

    document.getElementById('createBtn')!.style.display = 'none';
    document.getElementById('collapseBtn')!.style.display = 'none';
    document.getElementById('cancelBtn')!.style.display = 'none';
    document.getElementById('expandBtn')!.style.display = 'inline';
  }
}
