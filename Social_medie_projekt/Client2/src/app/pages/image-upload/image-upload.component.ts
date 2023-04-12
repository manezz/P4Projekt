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

  edit() {
    this.userService
      .updateUser(this.editUser.userId!, this.editUser)
      .subscribe((x) => (this.currentUser.user = x));
  }

  uploadImage(fileInput: any) {
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

          // test
          console.log(result.length);

          if (result.length === 1) {
            return result[0];
          } else if (result.length === 2) {
            return result[1];
          } else {
            throw console.error('Problem with Image');
          }
        };

        this.editUser.userImage!.image = imgBase64();

        this.edit();

        this.authService.refresh();
      };
    };
    reader.readAsDataURL(fileInput.target.files[0]);
  }
}
