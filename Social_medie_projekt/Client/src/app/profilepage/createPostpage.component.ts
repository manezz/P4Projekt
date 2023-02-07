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
    <img src="/assets/images/socialmachine.png" width="400px">

    <!-- <form [formGroup]="postForm" class="form" (ngSubmit)="create()"> -->
    <form class="form" (ngSubmit)="create()">
      
    <div class="formControl">
        <label>Title</label>
        <input type="text" id="FirstName" formControlName="Titel"/>
      </div>
      <div class="formControl">
        <label>Write your post</label>
        <input type="text" id="LastName" formControlName="Content"/>
      </div>
      <div class="formControl">
        <label>tags</label>
        <input type="text" id="Address" formControlName="Tags"/>
      </div>
      <div class="buttonDiv">
        <button id="createBtn">Create</button>
        <button type="button" (click)="cancel()" id="createBtn">Cancel</button>
      </div>  

    </form>

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
  .form{
    width: 100%;
    max-width: 800px;
    margin-top: 50px;
  }

  .formControl{
    display: flex;
    justify-content: center;
    margin: 5px 5px 10px 0;
    flex-direction: row;
  }
  label{
    order: 0;
    width: 100px;
    margin-right: 5px;
    text-align: right;
  }
  input{
    order: 1;
    width: 250px;
    margin-left: 3px;
    background-color: white;
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
export class CreatePostPageComponent{
    


  constructor(
    private auth: AuthService,
    private postService: PostService,
    private router: Router, 
    private route: ActivatedRoute, 
    private AppComponent: AppComponent,
  ){ }
    
  currentUser: any = {};
 
  post: Post = this.resetPost();
  posts: Post[] = []
  postForm: FormGroup = this.resetForm();


  ngOnInit(): void {
    this.resetForm()
    this.auth.currentUser.subscribe(x => { this.currentUser = x })
  }

  create(){
    
  }

  cancel(){

  }

  resetPost():Post {
    return{ postId: 0, title: '', desc: '', likes: 0, date: new Date, 
      user: { userId: 0, firstName: '', lastName: '', address: '', created: new Date, 
      login: { loginId: 1, email: '', password: '' }, 
      posts: [] } 
    }
  }

  resetForm(){
    return new FormGroup({
      Titel: new FormControl(''),
      Content: new FormControl(''),
      Tags: new FormControl('')
    })
  }

}
