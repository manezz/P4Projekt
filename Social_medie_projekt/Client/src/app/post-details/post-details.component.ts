import { Component } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { User } from '../_models/user';
import { PostService } from '../_services/post.service';
import { Post } from '../_models/post';

@Component({
  selector: 'app-post-details',
  template: `
  <div id="post">
    <h5 id="username"> 
      <img class="profilepic"src="./assets/images/placeholder.png" width="50" height="50">
      {{currentUser.loginResponse.user.userName}}
    </h5>
    <h1 id="title">{{post.title}}</h1>
    <h3 id="description">{{post.desc}}</h3>
    <!-- <p id="tags" *ngFor="let tag of tags">#{{tag.tag}}, </p> -->
    <p id="tags">#{{post.tags}}, </p>
    <p id="date">{{post.date | date:'MMM d yyyy, HH:mm a'}}</p> 
    <button class="postBtn" id="like"><3</button>
  </div>
  `,
  styleUrls: ["../_css/poststyle.css"]
})
export class PostDetailsComponent {

  currentUser: any = {};
  
  post: Post = {
    postId: 0,
    userId:0,
    title: '',
    desc: '',
    tags: '',
    likes: 0,
    date: new Date,
    user: {
      userId: 0, 
      userName: '',
      created: new Date, 
      login: {
        loginId: 1, 
        email: '', 
        password: ''
      }, 
      posts: []
    } 
  }



  constructor(private route: ActivatedRoute, private authService: AuthService, private postService: PostService){ }

  ngOnInit(): void {
    this.authService.currentUser.subscribe(x => this.currentUser = x)

    console.log(this.currentUser.loginResponse.user.userName)
    this.route.params.subscribe(params => { this.postService.GetPostByPostId(params['postId']).subscribe(x => this.post = x) })
  }

}
