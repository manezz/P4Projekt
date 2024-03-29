import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../_services/auth.service';
import { ImageUploadComponent } from '../image-upload/image-upload.component';

@Component({
  selector: 'app-update-userpage',
  standalone: true,
  imports: [CommonModule, RouterLink, ImageUploadComponent],
  templateUrl: 'update-userpage.component.html',
  styleUrls: ['update-userpage.component.css'],
})
export class UpdateUserPageComponent implements OnInit {
  currentUser: any = {};

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.currentUser.subscribe((x) => (this.currentUser = x));
  }
}
