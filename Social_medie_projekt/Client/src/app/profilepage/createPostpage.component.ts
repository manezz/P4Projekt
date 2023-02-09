import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../app.component';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Post } from '../_models/post';
import { PostService } from '../_services/post.service';
import { AuthService } from '../_services/auth.service';
import { FormGroup, FormsModule, FormControl, Validators } from '@angular/forms';




@Component({
  selector: 'app-createPostpage',
  template: `

  <div class="body">
    <button type="button" id="back" routerLink="">back</button>

    <div class="post">

      <!-- <h1 style="text-align: center; margin: 10px auto 40px auto"> Create your post here!</h1> -->
      
      <form [formGroup]="postForm" class="form" (ngSubmit)="create()">
        
        <div class="formControl">
          <textarea type="text" id="title" formControlName="Title" maxlength="100" (keyup)="maxLenght($event)" placeholder="Title"></textarea>
          <span id="titleCharLenght">{{titleCharLenght}}</span>
        </div>

        <div class="formControl">
          <textarea id="content" formControlName="Content" placeholder="Write about anything ..."></textarea>
        </div>

        <div class="formControl">
          <input type="text" id="tags" formControlName="Tags" placeholder="#Tags,"/>
        </div>
        
        <div class="buttonDiv">
          <button id="createBtn">Create</button>
          <button type="button" (click)="cancel()">Cancel</button>
        </div>  

      </form>

    </div>

  </div>
  `,

  styles: [`
  .body {
    display: flex; 
    height: 700px; 
    margin-top: 20px;
    align-items: center; 
    flex-direction: column;
  }
  #titleWrapper, .form, .formControl, .buttonDiv{
    background-color: lightgrey;
    border-radius: 15px;
  }
  .form{
    padding: 20px;
    width: 100%;
    max-width: 500px;
  }
  .formControl{
    position: relative; /* important for titleCharLenght*/
    /* display: flex; */
    /* flex-direction: row; */
  }
  input, textarea{
    width: 400px;
    height: 22px;
    padding: 30px 15px 5px 15px;
    border: none;
    border-bottom: 1px solid darkgray;
    font-size: 16px;
    resize: none;
    background-color: transparent;
  }
  input:focus, textarea:focus{
    outline: none;
    border-bottom: 1px solid gray;
  }
  #title{
    max-height: 50px;
    max-width: 400px;
  }
  #titleCharLenght{
    position: absolute;
    background-color: transparent;
    margin-top: 25px;
    right: 5px;
  }
  #content{
    height: 80px;
    max-width: 400px;
  }
  #tags{
    max-width: 400px;
  }




  .buttonDiv{
    display: flex;
    justify-content: center;
    margin-top: 20px;
  }
  button{
    width: 100px;
    margin-left: 5px;
    margin-right: 5px;
  }
  button:hover{
    background-color: rgb(211, 211, 211);
    cursor: pointer;
  }
  button:active{
    background-color: rgb(66, 66, 66);;
  }

  #back{
    position: absolute;
    margin-top: -5px;
    margin-left: 95%;
    width: 50px;
    height: 30px;
  }
  `]
})
export class CreatePostPageComponent implements OnInit{
    
  constructor(
    private auth: AuthService,
    private postService: PostService,
    private router: Router, 
    private route: ActivatedRoute, 
    private AppComponent: AppComponent,
  ){ }
  
  error: string
  currentUser: any = {};
  post: Post = this.resetPost();
  posts: Post[] = []
  postForm: FormGroup = this.resetForm();
  titleCharLenght: number //til at vise hvor mange tegn der kan være i post-title
  currentUserId: number

  ngOnInit(): void {
    this.resetForm()
    this.resetPost()
    this.titleCharLenght = 100
    this.auth.currentUser.subscribe(x => { this.currentUser = x })
    this.currentUserId = this.auth.CurrentUserValue.loginResponse.user.userId
  }

  create(){
    this.error = ''

    this.post = { 
      userId: this.currentUserId, 
      postId: 0, 
      title: this.postForm.value.Title, 
      desc: this.postForm.value.Content,
      tags: this.postForm.value.Tags,
    }

    this.postService.createPost(this.post).subscribe({
      next: (x) => {
        this.posts.push(x);
      },
      error: (err) => {
        console.warn(Object.values(err.error.errors).join(', '));
        this.error = Object.values(err.error.errors).join(', ');
        console.log(this.error)
      }
    });
    console.log(this.post)
  }

  cancel(){
    this.postForm = this.resetForm()
    this.post = this.resetPost()
    this.titleCharLenght = 100
  }

  resetPost():Post {
    // return{ userId: this.currentUserId, postId: 0, title: '', desc: '' }
    return{ 
      postId: 0,
      title: '', 
      desc: '', 
      tags: '', 
      date: new Date, 
      likes: 0, 
      user: { 
        userId: this.currentUserId, 
        userName: this.auth.CurrentUserValue.loginResponse.user.userName, 
        created: this.auth.CurrentUserValue.loginResponse.user.created 
      } 
    }
  }

  resetForm(){
    return new FormGroup({
      Title:    new FormControl(''),
      Content:  new FormControl(''),
      Tags:     new FormControl(''),
    })
  }

  maxLenght(event: any) {
    this.titleCharLenght = 100 - event.target.textLength;
    if(this.titleCharLenght <= 20)
      document.getElementById("titleCharLenght")!.style.color = "red"
    else
      document.getElementById("titleCharLenght")!.style.color = "black"
  }
  
}
