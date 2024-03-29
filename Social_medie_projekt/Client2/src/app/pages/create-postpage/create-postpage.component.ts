import { Component, Input, OnInit } from '@angular/core';
import { AppComponent } from '../../app.component';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule, SlicePipe } from '@angular/common';
import { Post } from '../../_models/post';
import { PostService } from '../../_services/post.service';
import { AuthService } from '../../_services/auth.service';
import { FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { Tag } from '../../_models/tag';
import { TaggedTemplateExpr } from '@angular/compiler';
import { EMPTY, take } from 'rxjs';

@Component({
  selector: 'app-create-postpage',
  standalone: true,
  imports: [CommonModule, RouterLink, ReactiveFormsModule, MatInputModule],
  templateUrl: 'create-postpage.component.html',
  styleUrls: ['create-postpage.component.css'],
})
export class CreatePostPageComponent implements OnInit {
  constructor(private auth: AuthService, private postService: PostService) {}
  @Input()
  posts: any;

  error: string | undefined;
  currentUser: any;
  currentUserId: number = 0;
  post: Post = this.resetPost();
  postForm: FormGroup = this.resetForm();
  titleCharLenght: number | undefined; //til at vise hvor mange tegn der kan være i post-title
  contentCharLenght: number | undefined; //til at vise hvor mange tegn der kan være i post-content
  splitTags: Tag[] = [];

  ngOnInit(): void {
    this.resetForm();
    this.resetPost();
    this.titleCharLenght = 100;
    this.auth.currentUser.subscribe((x) => (this.currentUser = x));
    this.currentUserId = this.auth.CurrentUserValue!.user!.userId!;
    this.collapseDiv();
  }

  create() {
    this.postForm.value.Tags.split(',').forEach((e: string) => {
      this.splitTags.push({ name: e });
    });

    this.post = {
      userId: this.currentUser.user.userId,
      postId: 0,
      title: this.postForm.value.Title,
      desc: this.postForm.value.Content,
      tags: this.splitTags,
    };

    this.postService.createPost(this.post).subscribe({
      next: (x) => {
        x.user!.userImage = this.currentUser.user.userImage;
        this.posts.unshift(x);
        this.postForm = this.resetForm();
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
    if (this.contentCharLenght <= 100) {
      document.getElementById('contentCharLenght')!.style.display = 'inline';
      document.getElementById('contentCharLenght')!.style.color = 'red';
    } else document.getElementById('contentCharLenght')!.style.display = 'none';
  }

  expandDiv() {
    document.getElementById('form')!.style.backgroundColor = 'lightgray';

    document.getElementById('title')!.style.display = 'inline';
    document.getElementById('titleCharLenght')!.style.display = 'inline';
    document.getElementById('content')!.style.display = 'inline';
    document.getElementById('contentCharLenght')!.style.display = 'inline';
    document.getElementById('contentCharLenght')!.style.color = 'black';
    document.getElementById('tags')!.style.display = 'inline';

    document.getElementById('createBtn')!.style.display = 'inline';
    document.getElementById('collapseBtn')!.style.display = 'inline';
    document.getElementById('cancelBtn')!.style.display = 'inline';
    document.getElementById('expandBtn')!.style.display = 'none';

    document.getElementById('createBtn')!.parentElement!.style.margin =
      '15px 0 15px 0';
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

    document.getElementById('createBtn')!.parentElement!.style.margin =
      '0 0 0 0';
  }
}
