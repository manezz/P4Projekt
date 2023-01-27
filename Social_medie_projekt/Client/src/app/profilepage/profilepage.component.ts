import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Post } from '../_models/post';
import { PostService } from '../_services/post.service';
import { User } from '../_models/user';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';


@Component({
  selector: 'app-profilepage',
  template: `

  <div id="post" *ngFor="">
    <h1 id="title"></h1>

  </div>
  `,
  styleUrls: ["../_css/poststyle.css"]
})
export class ProfilepageComponent implements OnInit{

  currentUser: any = {};

   user: User = {
     userId: 0,
     loginId:0,
     firstName: '',
     lastName: '',
     address: '',
     created: new Date,
     posts: [],
     login: {loginId: 0, email: '', password: ''}
   }

  
  // sætter values i getTempData til data
  constructor(private userService:UserService, private route: ActivatedRoute, private authService: AuthService){ }


  ngOnInit(): void {
   this.route.params.subscribe(params => {
     this.userService.getAllSelf(params['userId']).subscribe(x => this.user = x)
   })

   this.authService.currentUser.subscribe(x => {
    this.currentUser = x;
  })

  

   console.log(this.currentUser.loginResponse)
   console.log(this.user)


   

  }

  

}
