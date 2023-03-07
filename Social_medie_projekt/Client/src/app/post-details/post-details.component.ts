import { Component } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PostService } from '../_services/post.service';
import { Post } from '../_models/post';

@Component({
  selector: 'app-post-details',
  template: `
  <div id="post">
    <div id="user" (click)="postLink(this.post.user)"> 
      <img id="profilepic"src="./assets/images/placeholder.png" width="50" height="50">
      <h5 id="userName">{{post.user?.userName}}</h5>
    </div>
    <div id="content">
      <h1 id="title">{{post.title}}</h1>
      <h3 id="description">{{post.desc}}</h3>
      <p id="tags" *ngIf="post.tags">#{{post.tags}}, </p>
      <p id="date">{{post.date | date:'MMM d yyyy, HH:mm a'}}</p> 
    </div>
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
    } 
  }

  constructor(
    private route: ActivatedRoute, 
    private authService: AuthService, 
    private postService: PostService,
    private router: Router
  ){ }

  ngOnInit(): void  {
    this.authService.currentUser.subscribe(x => this.currentUser = x)      
    this.route.params.subscribe(params => { this.postService.GetPostByPostId(params['postId']).subscribe(x => this.post = x) })
  }

  postLink(user: any) {
    if(user.userId == this.currentUser.loginResponse.user.userId){
      // linker til brugerens egen profilside
      this.router.navigateByUrl('/profile')
    }
    else{
      // linker til en andens bruger profilside
      // skal også sende userId med for at finde useren
      this.router.navigate(['/profile/', user.userId])
    }
  }

}
