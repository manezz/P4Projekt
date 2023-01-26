import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, first, last, map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Role } from '../_models/role';
import { User } from '../_models/user';
import { Login } from '../_models/login';
import { EmailValidator } from '@angular/forms';
import { SignInResponse } from '../_models/signInResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<SignInResponse>;
  currentUser: Observable<SignInResponse>;

  constructor(private http: HttpClient) { 
    this.currentUserSubject = new BehaviorSubject<SignInResponse>(
      JSON.parse(sessionStorage.getItem('currentUser') as string)
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get CurrentUserValue(): SignInResponse {
    return this.currentUserSubject.value;
  }


  login(email: string, password: string){
    let authenticateUrl = `${environment.apiUrl}Login/authenticate`;
    return this.http.post<SignInResponse>(authenticateUrl, { "email": email, "password": password}).pipe(map(user => {
      sessionStorage.setItem('currentUser', JSON.stringify(user));

      this.currentUserSubject.next(user as SignInResponse);
      return user;
    }))
  }
  logout(){
    sessionStorage.removeItem('currentUser');
    this.currentUserSubject = new BehaviorSubject<SignInResponse>(JSON.parse(sessionStorage.getItem('currentUser') as string));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  register(email: string, password: string, firstName: string, lastName: string, address: string): Observable<any> {
    let registerUrl = `${environment.apiUrl}Login/Register`;
    return this.http.post(registerUrl, {"email": email, "password": password, "user": {"firstName": firstName, "lastName": lastName, "address": address}
  });
  }
}
