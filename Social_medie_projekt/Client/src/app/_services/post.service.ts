import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Post } from '../_models/post'
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  private readonly apiUrl = environment.apiUrl + 'Post' ;

  constructor(private http: HttpClient) { }


  // henter ALLE posts
  getAll(): Observable<Post[]>{
    return this.http.get<Post[]>(`${this.apiUrl}`)
  }

  // henter posts fra en bestemt bruger gennem deres id
  GetPostById(id: number): Observable<Post>{
    return this.http.get<Post>(`${this.apiUrl}/${id}`)
    
  }

  // Opretter et post
  createPost(user: User){
    return this.http.post<Post>(this.apiUrl, user)
  }

  //delete post
  delPost(){

  }

  //update post
  updPost(){

  }

}
