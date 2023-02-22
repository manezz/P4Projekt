import { Component } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { PostService } from '../_services/post.service';
import { Post } from '../_models/post';
import { FormGroup, FormsModule, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-editPost',
  template: `
  <div class="body">
    <div class="post">
      <form [formGroup]="postForm" id="form" (ngSubmit)="edit()">
        
        <div class="formControl">
          <textarea type="text" id="title" formControlName="Title" ng-model="title" maxlength="100" (keyup)="titleMaxLenght($event)" placeholder="Title"></textarea>
          <span id="titleCharLenght">{{titleCharLenght}}</span>
        </div>

        <div class="formControl">
          <textarea id="content" formControlName="Content" ng-model="content" maxlength="1000" (keyup)="contentMaxLenght($event)" placeholder="Write about anything ..." 
            cdkTextareaAutosize #autosize="cdkTextareaAutosize" cdkAutosizeMaxRows="20">
          </textarea>
          <span id="contentCharLenght">{{contentCharLenght}}</span>
        </div>

        <div class="formControl">
          <input type="text" id="tags" formControlName="Tags" ng-model="tags" placeholder="#Tags,"/>
        </div>
        
        <div class="buttonDiv">
          <button id="editBtn">Edit post</button>
        </div>  

        <div class="buttonDiv">
          <button id="deleteBtn" (click)="delete(this.post.postId)">🗑</button>
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
    box-shadow: 0px 0px 10px 20px rgb(80, 80, 100);
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
    font-size: 24px;
  }
  #titleCharLenght{
    position: absolute;
    background-color: transparent;
    margin-top: 25px;
    right: 25px;
  }
  #content{
    width: 100%;
    max-width: 85%;
    overflow: hidden;
    max-height: 200px;
  }
  #contentCharLenght{
    position: absolute;
    background-color: transparent;
    margin-top: 25px;
    right: 25px;
    color: red;
  }
  #tags{
    width: 100%;
    max-width: 85%;
    font-size: 12px;
    color: rgb(100,100,100);
  }



  .buttonDiv{
    position: relative;
    display: flex;
    justify-content: center;
    margin-top: 20px;
  }
  #editBtn{
    width: 150px;
    height: 35px;
    font-size: 25px;
  }
  #editBtn, #deleteBtn{
    border: none;
    border-radius: 15px;
  }
  #deleteBtn{
    width: 25px;
    height: 25px;
    position: absolute;
    bottom: 20px;
    right: 0px;
    font-size: 15px;
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
export class EditPostComponent {

  currentUser: any = {};
  postForm: FormGroup = this.resetForm()
  post: Post = this.resetPost()
  posts: Post[] = []
  titleCharLenght: number //til at vise hvor mange tegn der kan være i post-title
  contentCharLenght: number //til at vise hvor mange tegn der kan være i post-content

  constructor(
    private route: ActivatedRoute, 
    private authService: AuthService, 
    private postService: PostService,
    private router: Router,
  ){ 
  }
  
  ngOnInit() {
    this.authService.currentUser.subscribe(x => this.currentUser = x)
    this.route.params.subscribe(params => { this.postService.GetPostByPostId(params['postId']).subscribe(x => this.post = x) })

    console.log(this.post)
    
    this.insertValues()
  }

  insertValues(){
    (<HTMLInputElement>document.getElementById("title")).value = this.post.title;
    (<HTMLInputElement>document.getElementById("content")).value = this.post.desc;
    (<HTMLInputElement>document.getElementById("tags")).value = this.post.tags;
  }

  edit(){
    if(!this.postForm.pristine){
      this.post = {
        postId: this.post.postId,
        title: this.postForm.value.Title, 
        desc: this.postForm.value.Content,
        tags: this.postForm.value.Tags,
      }
      
      this.postService.editPost(this.post).subscribe()

      // routes back to current posts post-detail
      this.router.navigate(["/post-details/", this.post.postId])
    }
    else{
      this.insertValues()
      console.log("nothing changed")
      console.log(this.post)
    }    
  }

  delete(postId: number){
    if(confirm("Are you sure you want to delete this post?")){
      this.postService.deletePost(postId).subscribe({
        next: (x) => {
          this.posts.push(x);
          
            // routes back to mainpage
          this.router.navigate(["/main"])
        },
        error: (err) => {
          console.warn(Object.values(err.error.errors).join(', '))
        }
      });
    }
  }

  resetPost():Post {
    return{ postId: 0, title: '', desc: '', tags: '' }
  }

  resetForm(){
    return new FormGroup({ Title: new FormControl(''), Content: new FormControl(''), Tags: new FormControl('') })
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

}
