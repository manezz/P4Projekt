
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';
import { Login } from '../_models/login';
import { Role } from '../_models/role';
import { AuthService } from '../_services/auth.service';
import { AppComponent } from '../app.component';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-loginpage',
  template: `
    <div class="formControl">
      <img src="/assets/images/socialmachine.png" width="600">
      <div class="formDiv">
        <label>Email</label>
        <input type="email" [(ngModel)]="email"/>
      </div>
      <div class="formDiv">
        <label>Password</label>
        <input type="password"  [(ngModel)]="password"/>
      <!-- </div>
        <span class="error"*ngIf="userForm.get('userName')?.invalid && userForm.get('passHash')?.touched">Fill out form!</span>
      </div> -->
      <div class="buttonDiv">
        <button type="button" (click)='login()'>Login</button>
        <button class="right" routerLink="/login-create">Create new user</button>
      </div>
    </div>`,
  styles: [`.formControl {display:flex; height: 700px; justify-content: center; align-items: center; flex-direction: column;}`]
})
export class LoginpageComponent {

  constructor(private auth: AuthService,  private router: Router, private route: ActivatedRoute, private AppComponent: AppComponent) { }
  message = '';
  email: string = '';
  password: string = '';

  login(){
    this.auth.login(this.email, this.password).subscribe({

      next: () => {
        let returnUrl = this.route.snapshot.queryParams['returnUrl']||'/main';
        console.log(this.auth.CurrentUserValue)
        this.router.navigate([returnUrl])

        //ændrer headeren
        this.AppComponent.validateHeader()
      },
      
      error: err => {
        if (err.error?.status == 400 || err.error?.status == 401 || err.error?.status == 500) {
          this.message = 'Forkert brugernavn eller kodeord';
        }
        else {
          this.message = err.error.title;
        }
      }
      
    });  

  }
}

