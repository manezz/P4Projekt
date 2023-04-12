import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, first, last, map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Role } from '../_models/role';
import { User } from '../_models/user';
import { Login } from '../_models/login';
import { EmailValidator } from '@angular/forms';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<Login>;
  currentUser: Observable<Login>;

  // test
  cur: any[] = [];

  constructor(private http: HttpClient, private userService: UserService) {
    this.currentUserSubject = new BehaviorSubject<Login>(
      JSON.parse(sessionStorage.getItem('currentUser') as string)
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get CurrentUserValue(): Login {
    return this.currentUserSubject.value;
  }

  login(email: string, password: string) {
    const authenticateUrl = `${environment.apiUrl}Login/authenticate`;
    return this.http
      .post<Login>(authenticateUrl, { email: email, password: password })
      .pipe(
        map((login) => {
          sessionStorage.setItem('currentUser', JSON.stringify(login));
          this.currentUserSubject.next(login);
          return login;
        })
      );
  }

  refresh() {
    let newCurrentUser = this.CurrentUserValue;

    this.userService
      .getUser(this.CurrentUserValue.user!.userId!)
      .subscribe((x) => (newCurrentUser.user = x));

    sessionStorage.setItem('currentUser', JSON.stringify(newCurrentUser));
  }

  logout() {
    sessionStorage.removeItem('currentUser');
    this.currentUserSubject = new BehaviorSubject<Login>(
      JSON.parse(sessionStorage.getItem('currentUser') as string)
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  register(login: Login): Observable<Login> {
    const registerUrl = `${environment.apiUrl}login/register/`;
    return this.http.post<Login>(registerUrl, login);
  }
}
