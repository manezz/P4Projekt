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
          <textarea type="text" id="title" formControlName="Title" maxlength="100" (keyup)="titleMaxLenght($event)" placeholder="Title"></textarea>
          <span id="titleCharLenght">{{titleCharLenght}}</span>
        </div>

        <div class="formControl">
          <textarea id="content" formControlName="Content" maxlength="1000" (keyup)="contentMaxLenght($event)" placeholder="Write about anything ..." 
            cdkTextareaAutosize #autosize="cdkTextareaAutosize" cdkAutosizeMaxRows="20">
          </textarea>
          <span id="contentCharLenght">{{contentCharLenght}}</span>
        </div>

        <div class="formControl">
          <input type="text" id="tags" formControlName="Tags" placeholder="#Tags,"/>
        </div>
        
        <div class="buttonDiv">
          <button id="createBtn" (click)="create()">Create!</button>
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
    background-color: transparent;
    border-top: 3px solid lightgray;
    border-bottom: 3px solid lightgray;

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
    width: 100%;
    max-width: 85%;
    overflow: hidden;
    max-height: 200px;
    display: none;
  }
  #titleCharLenght{
    position: absolute;
    background-color: transparent;
    margin-top: 25px;
    right: 25px;
    display: none;
  }
  #content{
    width: 100%;
    max-width: 85%;
    overflow: hidden;
    max-height: 200px;
    display: none;
  }
  #contentCharLenght{
    position: absolute;
    background-color: transparent;
    margin-top: 25px;
    right: 25px;
    color: red;
    display: none;
  }
  #tags{
    width: 100%;
    max-width: 85%;
    display: none;
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
    font-size: 25px;
    display: none;
  }
  #createBtn, #expandBtn, #collapseBtn, #cancelBtn{
    border: none;
    border-radius: 15px;
  }
  #collapseBtn{
    width: 25px;
    height: 25px;
    position: absolute;
    bottom: 20px;
    right: 5px;
    display: none;
  }
  #cancelBtn{
    width: 25px;
    height: 25px;
    position: absolute;
    bottom: 20px;
    right: 35px;
    font-size: 15px;
    display: none;
  }
  #expandBtn{
    width: 150px;
    height: 35px;
    position: absolute;
    bottom: 15px;
    font-size: 25px;
  }
  button{
    background-color: darkgray;
  }
  button:hover{
    box-shadow: 0px 0px 10px 5px rgb(80, 80, 100);
    cursor: pointer;
  }
  button:active{
    background-color: rgb(66, 66, 66);
  }
  `]
})
export class CreatePostPageComponent implements OnInit{

  constructor(
    private auth: AuthService,
    private postService: PostService,
    private router: Router, 
    private route: ActivatedRoute, 
    private AppComponent: AppComponent
  ){ }
  
  error: string
  currentUser?: any = {}
  currentUserId?: number
  post: Post = this.resetPost()
  posts: Post[] = []
  postForm: FormGroup = this.resetForm()
  titleCharLenght: number //til at vise hvor mange tegn der kan være i post-title
  contentCharLenght: number //til at vise hvor mange tegn der kan være i post-content

  ngOnInit(): void {
    this.resetForm()
    this.resetPost()
    this.titleCharLenght = 100
    this.auth.currentUser.subscribe(x => { this.currentUser = x })
    this.currentUserId = this.auth.CurrentUserValue.user?.userId
    this.collapseDiv()
  }

  create(){
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
        console.warn(Object.values(err.error.errors).join(', '))
      }
    });
  }

  cancel(){
    this.postForm = this.resetForm()
    this.post = this.resetPost()
    this.titleCharLenght = 100
    this.contentCharLenght = 1000
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
        userName: this.auth.CurrentUserValue.user?.userName, 
        created: this.auth.CurrentUserValue.user?.created 
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



  titleMaxLenght(event: any) {
    this.titleCharLenght = 100 - event.target.textLength;
    if(this.titleCharLenght <= 20)
      document.getElementById("titleCharLenght")!.style.color = "red"
    else
      document.getElementById("titleCharLenght")!.style.color = "black"
  }

  contentMaxLenght(event: any) {
    this.contentCharLenght = 1000 - event.target.textLength;
    if(this.contentCharLenght <= 100)
    document.getElementById("contentCharLenght")!.style.display = 'inline'
    else
    document.getElementById("contentCharLenght")!.style.display = 'none'
  }


  expandDiv(){
    document.getElementById("form")!.style.backgroundColor = "lightgray"

    document.getElementById("title")!.style.display = 'inline'
      document.getElementById("titleCharLenght")!.style.display = 'inline'
    document.getElementById("content")!.style.display = 'inline'
      document.getElementById("contentCharLenght")!.style.display = 'inline'
    document.getElementById("tags")!.style.display = 'inline'

    document.getElementById("createBtn")!.style.display = 'inline'
    document.getElementById("collapseBtn")!.style.display = 'inline'
    document.getElementById("cancelBtn")!.style.display = 'inline'
    document.getElementById("expandBtn")!.style.display = 'none'
  }

  collapseDiv(){
    document.getElementById("form")!.style.backgroundColor = "transparent"
    document.getElementById("form")!.style.borderTop = "3px solid lightgray"
    document.getElementById("form")!.style.borderBottom = "3px solid lightgray"

    document.getElementById("title")!.style.display = "none"
      document.getElementById("titleCharLenght")!.style.display = "none"
    document.getElementById("content")!.style.display = "none"
      document.getElementById("contentCharLenght")!.style.display = "none"
    document.getElementById("tags")!.style.display = "none"
    
    document.getElementById("createBtn")!.style.display = "none"
    document.getElementById("collapseBtn")!.style.display = 'none'
    document.getElementById("cancelBtn")!.style.display = 'none'
    document.getElementById("expandBtn")!.style.display = 'inline'
  }
  
}

