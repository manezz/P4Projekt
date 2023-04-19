import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-follow-button',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: 'follow-button.component.html',
})
export class FollowButtonComponent {
  // @Input()
  // profileUser: User = {
  //   userName: '',
  //   userImage: {
  //     image: '',
  //   },
  // };

  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private userService: UserService
  ) {}
}
