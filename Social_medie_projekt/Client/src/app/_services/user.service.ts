import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Post } from '../_models/post'
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
    private readonly apiUrl = environment.apiUrl + 'User';

    constructor(private http: HttpClient) { }

    getAllUsers(): Observable <User[]>{
        return this.http.get<User[]>(this.apiUrl)
    }

    getAllSelf(userId: number): Observable<User> {
        return this.http.get<User>(`${this.apiUrl}/${userId}`)
    }

    createUser(user: User): Observable<User>{
        return this.http.post<User>(this.apiUrl, user)
    }
    
    updateUser(Id:number,user:User): Observable<User>{
        return this.http.put<User>(`${this.apiUrl}/${Id}`,user)
  }

}
