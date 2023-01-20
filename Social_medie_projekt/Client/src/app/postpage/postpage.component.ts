import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';
import { Login } from '../_models/login';
import { Role } from '../_models/role';
import { Post } from '../_models/post';
import { Tag } from '../_models/tags';
import { AuthService } from '../_services/auth.service';
import { AppComponent } from '../app.component';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { PostService } from '../_services/post.service';

@Component({
  selector: 'app-postpage',
  template: `
    <div class="formControl">
      <div class="formDiv">
        <label>Title</label>
        <input type="string" [(ngModel)]="title"/>
      </div>
      <div class="formDiv">
        <label>Decription</label>
        <input type="string"  [(ngModel)]="desc"/>
        <label>Tags</label>
        <input type="text" [(ngModel)]="tags"/>
        <label>Picture</label>
        <input type="string"  [(ngModel)]="pictureURL"/>
      <!-- </div>
        <span class="error"*ngIf="userForm.get('userName')?.invalid && userForm.get('passHash')?.touched">Fill out form!</span>
      </div> -->
      <div class="buttonDiv">
        <button type="button" (click)='posting()'>Post</button>
        <button class="right" routerLink="/main">Cancel</button>
      </div>
    </div>`,
  styles: [`.formControl {display:flex; height: 700px; justify-content: center; align-items: center; flex-direction: column;}`]
})
export class PostPageComponent {
    constructor(private auth: AuthService, private router: Router, private route: ActivatedRoute, private AppComponent: AppComponent, private post: PostService) {}
    title: string = '';
    date: string = '';
    desc: string = '';
    pictureURL: string = '';
    tags: Tag[] = [];

    posting(){
        this.post.createPost(this.title, this.date, this.desc, this.tags, this.pictureURL)
    }
}