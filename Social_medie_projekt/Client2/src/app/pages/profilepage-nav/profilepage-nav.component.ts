import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ImageComponent } from '../image/image.component';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { User } from 'src/app/_models/user';
import { FollowButtonComponent } from '../follow-button/follow-button.component';

@Component({
  selector: 'app-profilepage-nav',
  standalone: true,
  imports: [CommonModule, RouterModule, ImageComponent, FollowButtonComponent],
  templateUrl: 'profilepage-nav.component.html',
  styleUrls: ['profilepage-nav.component.css'],
})
export class ProfilepageNavComponent {
  @Input()
  currentUser: any;

  @Input()
  profileUser: User = {
    userName: '',
    userImage: {
      image: '',
    },
  };

  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private userService: UserService
  ) {}
}
