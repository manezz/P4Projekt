import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../../app.component';
import { RouterModule, ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Login } from '../../_models/login';
import { UserService } from '../../_services/user.service';
import { AuthService } from '../../_services/auth.service';
import { FormGroup, FormsModule, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
// import { MatDialog } from '@angular/material/dialog';
// import { SuccessPopup } from './createPagePopup';

@Component({
  selector: 'app-createUserpage',
  standalone: true,
  templateUrl: "create-userpage.component.html",
  styleUrls: ["create-userpage.component.css"],
  imports: [CommonModule, ReactiveFormsModule],
  
})
export class CreateUserPageComponent implements OnInit {
  constructor(
    private auth: AuthService,
    private userService: UserService,
    private router: Router,
    private route: ActivatedRoute,
    private AppComponent: AppComponent
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

        // user gets logged in right away
        this.auth
          .login(this.userForm.value.Email, this.userForm.value.Password)
          .subscribe({
            next: () => {
              let returnUrl =
                this.route.snapshot.queryParams['returnUrl'] || '/main';
              this.router.navigate([returnUrl]);
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
}