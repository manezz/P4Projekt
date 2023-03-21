import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-createpage-popup',
  template: `
    <div class="dialog-container">
      <h1 mat-dialog-title>Success!</h1>
      <div mat-dialog-content>
        <p>.</p>
        <p>.</p>
        <p>.</p>
        <p>.</p>
        <p>User created succesfully!</p>
        <p>.</p>
        <p>.</p>
        <p>.</p>
        <p>.</p>
      </div>
    </div>
  `,
  styles: [
    `
      h1,
      p {
        text-align: center;
      }
    `,
  ],
})
export class SuccessPopup {
  constructor(public dialogRef: MatDialogRef<SuccessPopup>) {}

  ngOnInit() {
    setTimeout(() => {
      this.dialogRef.close();
    }, 4000);
  }
}
