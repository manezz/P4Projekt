import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, FormsModule, Validators, FormControl, EmailValidator, PatternValidator } from '@angular/forms';
import { Login } from '../_models/login';
import { Role } from '../_models/role';
import { User } from '../_models/user';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { AppComponent } from '../app.component';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';

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
    .form{
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
    private AppComponent: AppComponent) { 
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
      FirstName: new FormControl(null, Validators.required),
      LastName: new FormControl(null, Validators.required),
      Address: new FormControl(null, Validators.required),
      Email: new FormControl(null, [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]),
      Password: new FormControl(null,  Validators.required),
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
      
      console.log(this.login)
      this.userService.createUser(this.login).subscribe({
        next: (x) => {
          this.logins.push(x);


          //logger brugeren ind med det samme
          this.auth.login(this.userForm.value.Email, this.userForm.value.Password).subscribe({

            next: () => {
              let returnUrl = this.route.snapshot.queryParams['returnUrl']||'/main';
              this.router.navigate([returnUrl])
              //ændrer headeren
              this.AppComponent.validateHeader()
            }            
          });

        },
        error: (err) => {
          console.warn(Object.values(err.error.errors).join(','));
          this.errors = Object.values(err.error.errors).join(',');
        }
      });
    }
  }

  cancel(){
    this.login = this.resetUser();
    this.userForm = this.resetForm();
  }
}
