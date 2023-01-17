import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-loginpage',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="formControl">
      <img src="/assets/images/socialmachine.png" width="600">
      <div class="formControl formDiv">
        <label>Username</label>
        <input type="text" formControlName="userName"/>
      </div>
      <div class="formControl formDiv">
        <label>Password</label>
        <input type="password"formControlName="passHash"/>
      <!-- </div>
        <span class="error"*ngIf="userForm.get('userName')?.invalid && userForm.get('passHash')?.touched">Fill out form!</span>
      </div> -->
      <div class="buttonDiv">
        <button>Login</button>
        <button class="right" routerLink="/login-create">Create new user</button>
      </div>
    </div>`,
  styles: [`formControl { display: flex; align-content: center; background: red;}`]
})
export class LoginpageComponent {

}
