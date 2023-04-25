import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { User } from 'src/app/_models/user';
import { ImageComponent } from '../image/image.component';

@Component({
  selector: 'app-image-upload',
  standalone: true,
  imports: [CommonModule, RouterModule, ImageComponent],
  templateUrl: 'image-upload.component.html',
  styleUrls: ['image-upload.component.css'],
})
export class ImageUploadComponent implements OnInit {
  currentUser: any = {};
  newImage: string = '';
  editUser: User = {
    userId: 0,
    userName: '',
    userImage: {
      image: '',
    },
  };
  imageClass: string = 'profileUpdateUserImage';

  constructor(
    private router: Router,
    private authService: AuthService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.authService.currentUser.subscribe(
      (x) => ((this.currentUser = x), (this.editUser = x.user!))
    );
  }

  edit(): void {
    this.userService
      .updateUser(this.editUser.userId!, this.editUser)
      .subscribe((x) => (this.currentUser.user = x));
  }

  uploadImage(fileInput: any): void {
    const reader = new FileReader();

    reader.onload = (e: any) => {
      const image = new Image();
      image.src = e.target.result;

      image.onload = (rs: any) => {
        const imgHeight = rs.currentTarget['height'];
        const imgWidth = rs.currentTarget['width'];

        console.log(imgWidth, imgHeight);

        const imgBase64 = (): string => {
          const result: string[] = e.target.result.split(',');
          return result[1];
        };

        this.editUser.userImage!.image = imgBase64();

        this.edit();

        // refreshes the localstorage user
        this.authService.refresh();
      };
    };
    reader.readAsDataURL(fileInput.target.files[0]);
  }
}
