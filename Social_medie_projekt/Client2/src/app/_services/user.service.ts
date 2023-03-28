import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Post } from '../_models/post';
import { User } from '../_models/user';
import { Login } from '../_models/login';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly apiUrl = environment.apiUrl + 'User';

  constructor(private http: HttpClient) {}

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  getUser(userId: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${userId}`)
}

  updateUser(Id: number, user: User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/${Id}`, user);
  }
}
