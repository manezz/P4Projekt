import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../app.component';
import {
  RouterModule,
  ActivatedRoute,
  Router,
  RouterLink,
} from '@angular/router';
import { CommonModule } from '@angular/common';
import { Login } from '../_models/login';
import { UserService } from '../_services/user.service';
import { AuthService } from '../_services/auth.service';
import {
  FormGroup,
  FormsModule,
  FormControl,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { SuccessPopup } from './createPagePopup';

@Component({
  selector: 'app-createUserpage',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  template: `
    <div class="body">
      <button type="button" id="back" routerLink="">back</button>
      <img src="/assets/images/socialmachine.png" width="400px" />
      <p>Create your user!</p>

      <form [formGroup]="userForm" class="form" (ngSubmit)="create()">
        <div class="formControl">
          <label>Username</label>
          <input type="text" id="UserName" formControlName="UserName" />
        </div>
        <div class="formControl">
          <label>Email</label>
          <input type="text" id="Email" formControlName="Email" />
        </div>
        <div class="formControl">
          <label>Password</label>
          <input type="password" id="Password" formControlName="Password" />
        </div>
        <div class="buttonDiv">
          <button [disabled]="!userForm.valid" id="createBtn">Create</button>
          <button type="button" (click)="cancel()" id="createBtn">
            Cancel
          </button>
        </div>
      </form>
    </div>
  `,
  styles: [
    `
      .body {
        display: flex;
        height: 700px;
        margin-top: 20px;
        align-items: center;
        flex-direction: column;
      }
      .form {
        width: 100%;
        max-width: 400px;
        margin-top: 50px;
      }

      .formControl {
        display: flex;
        justify-content: center;
        margin: 5px 5px 10px 0;
        flex-direction: row;
      }
      label {
        order: 0;
        width: 100px;
        margin-right: 5px;
        text-align: right;
      }
      input {
        order: 1;
        width: 250px;
        margin-left: 3px;
        background-color: white;
      }

      .buttonDiv {
        display: flex;
        justify-content: center;
        margin-top: 20px;
      }
      button {
        width: 100px;
        margin-left: 5px;
        margin-right: 5px;
      }
      button:hover {
        background-color: rgb(211, 211, 211);
        cursor: pointer;
      }
      button:active {
        background-color: rgb(66, 66, 66);
      }

      #back {
        position: absolute;
        margin-top: -5px;
        margin-left: 95%;
        width: 50px;
        height: 30px;
      }
    `,
  ],
})
export class CreateUserPageComponent implements OnInit {
  constructor(
    private auth: AuthService,
    private userService: UserService,
    private router: Router,
    private route: ActivatedRoute,
    private AppComponent: AppComponent,
    public dialog: MatDialog
  ) {}

  errors = '';
  login: Login = this.resetUser();
  logins: Login[] = [];
  userForm: FormGroup = this.resetForm();

  ngOnInit() {
    this.resetUser();
    this.resetForm();
  }

  resetUser(): Login {
    return {
      loginId: 0,
      email: '',
      password: '',
      user: { userId: 0, userName: '' },
    };
  }

  resetForm(): FormGroup {
    return new FormGroup({
      UserName: new FormControl(null, Validators.required),
      Email: new FormControl(null, [
        Validators.required,
        Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
      ]),
      Password: new FormControl(null, Validators.required),
    });
  }

  create(): void {
    this.login = {
      loginId: 0,
      email: this.userForm.value.Email,
      password: this.userForm.value.Password,
      user: {
        userId: 0,
        userName: this.userForm.value.UserName,
      },
    };

    this.auth.register(this.login).subscribe({
      next: (x) => {
        this.logins.push(x);

        // opens popup window
        this.dialog.open(SuccessPopup, {
          width: '750px',
          height: '400px',
          panelClass: 'dialog-container',
          position: { left: '30%', top: '-600px' },
        });

        // user gets logged in right away
        this.auth
          .login(this.userForm.value.Email, this.userForm.value.Password)
          .subscribe({
            next: () => {
              let returnUrl =
                this.route.snapshot.queryParams['returnUrl'] || '/main';
              this.router.navigate([returnUrl]);
              this.AppComponent.validateHeader();
            },
          });
      },
      error: (err) => {
        // change inputfields to red border
        document.getElementById('UserName')!.style.borderColor = 'red';
        document.getElementById('Email')!.style.borderColor = 'red';
        document.getElementById('Password')!.style.borderColor = 'red';

        console.warn(Object.values(err.error.errors).join(', '));
      },
    });
  }

  cancel() {
    this.login = this.resetUser();
    this.userForm = this.resetForm();

    // change inputfields borders back
    document.getElementById('UserName')!.style.borderColor = 'black';
    document.getElementById('Email')!.style.borderColor = 'black';
    document.getElementById('Password')!.style.borderColor = 'black';
  }

  popup() {
    // opens popup window
    this.dialog.open(SuccessPopup, {
      width: '750px',
      height: '400px',
      panelClass: 'dialog-container',
      position: { left: '30%', top: '-600px' },
    });
  }
}
