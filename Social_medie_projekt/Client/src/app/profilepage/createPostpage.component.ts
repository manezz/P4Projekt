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

    <div class="post">

      <form [formGroup]="postForm" id="form">
        
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
          <button id="createBtn" (ngSubmit)="create()">Create!</button>
        </div>  

        <div class="buttonDiv">
          <button id="expandBtn" (click)="expandDiv()">+</button>
          <button id="cancelBtn" (click)="cancel()">🗑</button>
          <button id="collapseBtn" (click)="collapseDiv()">▲</button>
        </div>  

      </form>

    </div>

  </div>
  `,

  styles: [`
  .body {
    display: flex;
    margin-top: 20px;
    align-items: center; 
    flex-direction: column;
  }
  .post{
    width: 100%;
    max-width: 37%;
  }
  #form, .formControl, .buttonDiv{
    background-color: lightgrey;
    border-radius: 15px;
    position: relative; /* important for titleCharLenght*/
  }
  #form{
    padding: 20px 20px 0px 20px;
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
    width: 100%;
    max-width: 85%;
  }
  #titleCharLenght{
    position: absolute;
    background-color: transparent;
    margin-top: 25px;
    right: 5px;
  }
  #content{
    /* height: 80px; */
    width: 100%;
    max-width: 85%;
  }
  #tags{
    width: 100%;
    max-width: 85%;
  }



  .buttonDiv{
    position: relative;
    display: flex;
    justify-content: center;
    margin-top: 20px;
  }
  #createBtn{
    width: 150px;
    height: 35px;
    border: none;
    border-radius: 15px;
    font-size: 25px;
  }
  #expandBtn, #collapseBtn, #cancelBtn{
    border: none;
    border-radius: 15px;
  }
  #collapseBtn{
    width: 25px;
    height: 25px;
    position: absolute;
    bottom: 20px;
    right: 5px;
  }
  #cancelBtn{
    width: 25px;
    height: 25px;
    position: absolute;
    bottom: 20px;
    right: 35px;
    font-size: 15px;
  }
  #expandBtn{
    background-color: darkgray;
    width: 60px;
    height: 30px;
    position: absolute;
    bottom: 15px;
    font-size: 25px;
  }
  button:hover{
    background-color: rgb(80, 80, 180);
    cursor: pointer;
  }
  button:active{
    background-color: rgb(66, 66, 66);;
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
  isClicked: boolean

  ngOnInit(): void {
    this.resetForm()
    this.resetPost()
    this.titleCharLenght = 100
    this.auth.currentUser.subscribe(x => { this.currentUser = x })
    this.currentUserId = this.auth.CurrentUserValue.loginResponse.user.userId
    this.collapseDiv()
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
    // this.collapseDiv()
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


  expandDiv(){
    document.getElementById("form")!.style.backgroundColor = "lightgray"

    document.getElementById("title")!.style.display = ''
    document.getElementById("titleCharLenght")!.style.display = ''
    document.getElementById("content")!.style.display = ''
    document.getElementById("tags")!.style.display = ''

    document.getElementById("createBtn")!.style.display = ''
    document.getElementById("collapseBtn")!.style.display = ''
    document.getElementById("cancelBtn")!.style.display = ''
    document.getElementById("expandBtn")!.style.display = 'none'
  }

  collapseDiv(){
    document.getElementById("form")!.style.backgroundColor = "transparent"
    document.getElementById("form")!.style.borderTop = "3px solid lightgray"
    document.getElementById("form")!.style.borderBottom = "3px solid lightgray"

    document.getElementById("title")!.style.display = "none"
    document.getElementById("titleCharLenght")!.style.display = "none"
    document.getElementById("content")!.style.display = "none"
    document.getElementById("tags")!.style.display = "none"
    
    document.getElementById("createBtn")!.style.display = "none"
    document.getElementById("collapseBtn")!.style.display = 'none'
    document.getElementById("cancelBtn")!.style.display = 'none'
    document.getElementById("expandBtn")!.style.display = ''
  }
  
}

