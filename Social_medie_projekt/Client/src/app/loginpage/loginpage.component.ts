import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-loginpage',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="formControl">
      <img src="/assets/images/socialmachine.png" width="600">
      <div class="formDiv">
        <label>Email</label>
        <input type="text"/>
      </div>
      <div class="formDiv">
        <label>Password</label>
        <input type="password"/>
      <!-- </div>
        <span class="error"*ngIf="userForm.get('userName')?.invalid && userForm.get('passHash')?.touched">Fill out form!</span>
      </div> -->
      <div class="buttonDiv">
        <button>Login</button>
        <button class="right" routerLink="/login-create">Create new user</button>
      </div>
    </div>`,
  styles: [`.formControl {display:flex; height: 700px; justify-content: center; align-items: center; flex-direction: column;}`]
})
export class LoginpageComponent {

}
