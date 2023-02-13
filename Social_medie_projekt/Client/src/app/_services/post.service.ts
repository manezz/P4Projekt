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

  private readonly apiUrl = environment.apiUrl + 'Post'

  constructor(private http: HttpClient) { }


  // henter ALLE posts
  getAll(): Observable<Post[]>{
    return this.http.get<Post[]>(`${this.apiUrl}`)
  }

  // henter en bestemt post
  GetPostByPostId(postId: number): Observable<Post>{
    return this.http.get<Post>(`${this.apiUrl}/${postId}`)
  }

  // henter alle posts fra en user
  GetPostByUserId(userid: number): Observable<Post[]>{
    return this.http.get<Post[]>(`${this.apiUrl}/user/${userid}`)
  }

  // Opretter et post
  createPost(post: Post){
    return this.http.post<Post>(this.apiUrl, post)
  }

  //delete post
  delPost(){

  }

  //update post
  editPost(post: Post){
    return this.http.put<Post>(this.apiUrl, post)
  }

}
