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

@Component({
  selector: 'app-createUserpage',
  template: `
  <div class="body">
    <button type="button" id="back" routerLink="">back</button>
    <img src="/assets/images/socialmachine.png" width="400px">
    <p> Create your user! </p>
    <form [formGroup]="userForm" class="form" (ngSubmit)="create()">
      <div class="formControl">
        <label>First name</label>
        <input type="text" id="FirstName" formControlName="FirstName"/>
      </div>
      <div class="formControl">
        <label>Last name</label>
        <input type="text" id="LastName" formControlName="LastName"/>
      </div>
      <div class="formControl">
        <label>Address</label>
        <input type="text" id="Address" formControlName="Address"/>
      </div>
      <div class="formControl">
        <label>Email</label>
        <input type="text" id="Email" formControlName="Email"/>
      </div>
      <div class="formControl">
        <label>Password</label>
        <input type="password" id="Password" formControlName="Password"/>
      </div>
      <div class="buttonDiv">
        <button [disabled]="!userForm.valid" id="createBtn">Create</button>
        <button type="button" (click)="cancel()" id="createBtn">Cancel</button>
      </div>  
    </form>

  </div>
  `,
  styles: [`
  .body {
    display: flex; 
    height: 700px; 
    margin-top: 20px;
    align-items: center; 
    flex-direction: column;
  }
  .form{
    width: 100%;
    max-width: 400px;
    margin-top: 50px;
  }

  .formControl{
    display: flex;
    justify-content: center;
    margin: 5px 5px 10px 0;
    flex-direction: row;
  }
  label{
    order: 0;
    width: 100px;
    margin-right: 5px;
    text-align: right;
  }
  input{
    order: 1;
    width: 250px;
    margin-left: 3px;
    background-color: white;
  }


  .buttonDiv{
    display: flex;
    justify-content: center;
    margin-top: 20px;
  }
  button{
    width: 100px;
    margin-left: 5px;
    margin-right: 5px;
  }
  button:hover{
    background-color: rgb(211, 211, 211);
    cursor: pointer;
  }
  button:active{
    background-color: rgb(66, 66, 66);;
  }

  #back{
    position: absolute;
    margin-top: -5px;
    margin-left: 95%;
    width: 50px;
    height: 30px;
  }
  `]
})
export class CreateUserPageComponent implements OnInit {

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
    return { loginId:0, email: "", password:"", user:{ userId:0, firstName:"", lastName:"", address:"" } }
  }
  
  resetForm(): FormGroup{
    return new FormGroup({
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
          // change inputfields to red border
          document.getElementById("FirstName")!.style.borderColor = "red"
          document.getElementById("LastName")!.style.borderColor = "red"
          document.getElementById("Address")!.style.borderColor = "red"
          document.getElementById("Email")!.style.borderColor = "red"
          document.getElementById("Password")!.style.borderColor = "red"

          console.warn(Object.values(err.error.errors).join(', '));
          this.errors = Object.values(err.error.errors).join(', ');
        }
      });
    }
  }

  cancel(){
    this.login = this.resetUser();
    this.userForm = this.resetForm();
    
    // change inputfields borders back
    document.getElementById("FirstName")!.style.borderColor = "black"
    document.getElementById("LastName")!.style.borderColor = "black"
    document.getElementById("Address")!.style.borderColor = "black"
    document.getElementById("Email")!.style.borderColor = "black"
    document.getElementById("Password")!.style.borderColor = "black"
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