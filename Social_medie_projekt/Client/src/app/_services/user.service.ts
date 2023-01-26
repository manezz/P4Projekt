import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user'

@Injectable({
  providedIn: 'root'
})
export class UserService {
    private readonly apiUrl = environment.apiUrl + 'User';

    constructor(private http: HttpClient) { }

    getAllUsers(): Observable <User[]>{
        return this.http.get<User[]>(this.apiUrl)
    }
    
    getUserById(userId: number): Observable<User> {
        return this.http.get<User>(`${this.apiUrl}/${userId}`)
    }

    createUser(user: User): Observable<User>{
        return this.http.post<User>(this.apiUrl, user)
    }

    updateUser(Id:number,user:User): Observable<User>{
        return this.http.put<User>(`${this.apiUrl}/${Id}`,user)
    }

}
