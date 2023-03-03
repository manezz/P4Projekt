import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { BehaviorSubject, first, last, map, Observable } from 'rxjs'
import { environment } from 'src/environments/environment'
import { Login } from '../_models/login'

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject?: BehaviorSubject<Login>
  currentUser: Observable<Login>

  constructor(private http: HttpClient) { 
    this.currentUserSubject = new BehaviorSubject<Login>(
      JSON.parse(sessionStorage.getItem('currentUser') as string)
    )
    this.currentUser = this.currentUserSubject.asObservable()
  }

  public get CurrentUserValue(): Login {
    return this.currentUserSubject!.value
  }





  public getUser(): any {
    const user = window.sessionStorage.getItem('currentUser')
    if (user) {
      return JSON.parse(user)
    }
    return {}
  }


  login(email: string, password: string){
    let authenticateUrl = `${environment.apiUrl}Login/authenticate`
    return this.http.post<Login>(authenticateUrl, { "email": email, "password": password}).pipe(map(user => {
      sessionStorage.setItem('currentUser', JSON.stringify(user))
      this.currentUserSubject?.next(user as Login)
    }))
  }

  logout(){
    sessionStorage.removeItem('currentUser');
    this.currentUserSubject   = new BehaviorSubject<Login>(JSON.parse(sessionStorage.getItem('currentUser') as string))
    this.currentUser          = this.currentUserSubject.asObservable()
  }

  register(login: Login): Observable<Login> {
    let registerUrl = `${environment.apiUrl}login/register/`
    return this.http.post<Login>(registerUrl, login)
  }
}
