import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-image-upload',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: 'image-upload.component.html',
})
export class ImageUploadComponent {
  constructor() {}

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
