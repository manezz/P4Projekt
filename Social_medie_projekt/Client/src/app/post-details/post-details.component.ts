import { Component } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { User } from '../_models/user';
import { PostService } from '../_services/post.service';
import { Post } from '../_models/post';

@Component({
  selector: 'app-post-details',
  templateUrl: './post-details.component.html',
  styleUrls: ["../_css/poststyle.css"]
})
export class PostDetailsComponent {

  currentUser: any = {};
  
  post: Post = {
    postId: 0,
    userId:0,
    title: '',
    desc: '',
    likes: 0,
    date: new Date,
    user: {userId: 0, loginId: 1, firstName: '', lastName: '', address: '', created: new Date, login: {loginId: 1, email: '', password: ''}, posts: []}

  }



  constructor(private route: ActivatedRoute, private authService: AuthService, private postService: PostService){ }

  ngOnInit(): void {

    this.authService.currentUser.subscribe(x => {
     this.currentUser = x;
   })

   this.route.params.subscribe((params) => {
     this.postService
     .GetPostById(params['postId'])
     .subscribe((x) => (this.post = x));
   })

   console.log()
  
  }

}
