import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, FormsModule,  FormControl, Validators } from '@angular/forms';
import { Login } from '../_models/login';
import { User } from '../_models/user';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { AppComponent } from '../app.component';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatButton } from '@angular/material/button';
import { MatFormField } from '@angular/material/form-field';
import { delay } from 'rxjs';

@Component({
  selector: 'app-createpage',
  template: `
  <div class="body">
    <button type="button" id="back" routerLink="">back</button>
    <img src="/assets/images/socialmachine.png" width="25%">
    <p> Create your user! </p>
    
    <form [formGroup]="userForm" (ngSubmit)="create()">
      <!-- <div class="formControl">
        <label>Username</label>
        <input type="text" formControlName="UserName"/>
      </div> -->
      <div class="formControl">
        <label>First name</label>
        <input type="text" formControlName="FirstName"/>
      </div>
      <div class="formControl">
        <label>Last name</label>
        <input type="text" formControlName="LastName"/>
      </div>
      <div class="formControl">
        <label>Address</label>
        <input type="text" formControlName="Address"/>
      </div>
      <div class="formControl">
        <label>Email</label>
        <input type="text" formControlName="Email"/>
      </div>
      <div class="formControl">
        <label>Password</label>
        <input type="text" formControlName="Password"/>
      </div>
      <div class="buttonDiv">
        <!-- <button [disabled]="!userForm.valid" id="createBtn">Create</button> -->
        <button id="createBtn">Create</button>
        <button (click)="cancel()" id="createBtn">Cancel</button>
        <button (click)="popup()" id="createBtn">popup test</button>
      </div>  
    </form>

  </div>
  `,
  styles: [`
  .body {
    display: flex; 
    align-items: center; 
    flex-direction: column;
  }
  .userForm{
    display: blocK;
    width: 30%;
    margin-left: auto;
    margin-right: auto
  }
  .formControl{
    width: 100%;
    margin: 5px;
    text-align: right;
  }
  input{
    width: 50%;
    margin-left: 10px;
    background-color: white;
  }
  .buttonDiv{
    width: 100%;
    align-items: center;
  }
  #createBtn{
    width: 100px;
  }
  button{
    margin-left: 5px;
  }
  #back{
    position: fixed;
    margin-top: 15px;
    margin-left: 95%;
  }
  `]
})
export class CreatepageComponent {

  constructor(
    private auth: AuthService,
    private userService: UserService, 
    private router: Router, 
    private route: ActivatedRoute, 
    private AppComponent: AppComponent,
    public dialog: MatDialog) { 
  }

  errors = '';
  login: Login = this.resetUser();
  logins: Login[] = []
  userForm: FormGroup = this.resetForm();

  ngOnInit(){
    this.resetUser()
    this.resetForm()
  }

  resetUser():Login{
    // return { userId:0, userName="", firstName:"", lastName:"", address:"", login:{ loginId:0, email: "", password:"" } }
    return { loginId:0, email: "", password:"", user:{ userId:0, firstName:"", lastName:"", address:"" } }
  }
  
  resetForm(): FormGroup{
    return new FormGroup({
      // UserName: new FormControl(null, Validators.required),
      FirstName: new FormControl(null,  Validators.required),
      LastName:  new FormControl(null,  Validators.required),
      Address:   new FormControl(null,  Validators.required),
      Email:     new FormControl(null, [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]),
      Password:  new FormControl(null,  Validators.required),
    })
  }



  create():void{
    if(true){

      this.login = { 
        loginId:0, 
        email: this.userForm.value.Email, 
        password: this.userForm.value.Password,
        user:{ 
          userId:0, 
          firstName: this.userForm.value.FirstName, 
          lastName: this.userForm.value.LastName, 
          address: this.userForm.value.Address
        }
      }
      
      this.userService.createUserOnLogin(this.login).subscribe({
        next: (x) => {
          this.logins.push(x);

          // opens popup window
          this.dialog.open(SuccessPopup, {
            width: '750px',
            height: '400px',
            panelClass: 'dialog-container',
            position: {left: '30%', top: '-600px'}
          });

          // user gets logged in right away
          this.auth.login(this.userForm.value.Email, this.userForm.value.Password).subscribe({
            next: () => {
              let returnUrl = this.route.snapshot.queryParams['returnUrl']||'/main';
              this.router.navigate([returnUrl])
              this.AppComponent.validateHeader()
            }
          });
        },
        error: (err) => {
          console.warn(Object.values(err.error.errors).join(', '));
          this.errors = Object.values(err.error.errors).join(', ');
        }
      });
    }
  }

  cancel(){
    this.login = this.resetUser();
    this.userForm = this.resetForm();
  }  

  popup(){
    // opens popup window
    this.dialog.open(SuccessPopup, {
      width: '750px',
      height: '400px',
      panelClass: 'dialog-container',
      position: {left: '30%', top: '-600px'}
    });
  }
}

@Component({
  selector: 'app-createpage-popup',
  template: `
  <div class='dialog-container'>
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
  styles: [`
  h1, p{
    text-align: center;
  }
`]
})
export class SuccessPopup {

  constructor( public dialogRef: MatDialogRef<SuccessPopup> ) {}

  ngOnInit(){
    setTimeout(() => {
      this.dialogRef.close()
   }, 4000);
  }

}