import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Post } from '../_models/post'
import { User } from '../_models/user';
import { DataService } from '../_services/data.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly apiUrl = environment.apiUrl + 'User' ;

  constructor(private http: HttpClient) { }

  getAllSelf(id: number): Observable<User>{
    return this.http.get<User>(`${this.apiUrl}/${id}`)
    
  }
}
