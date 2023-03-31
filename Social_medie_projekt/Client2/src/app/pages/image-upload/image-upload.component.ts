import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-image-upload',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: 'image-upload.component.html',
})
export class ImageUploadComponent {
  constructor(private router: Router, private userService: UserService) {}

  uploadImage(fileInput: any) {
    const reader = new FileReader();

    reader.onload = (e: any) => {
      const image = new Image();
      image.src = e.target.result;

      image.onload = (rs: any) => {
        const imgHeight = rs.currentTarget['height'];
        const imgWidth = rs.currentTarget['width'];

        console.log(imgHeight, imgWidth);

        const imgBase64 = e.target.result;

        console.log(imgBase64);
      };
    };
    reader.readAsDataURL(fileInput.target.files[0]);
  }
}
