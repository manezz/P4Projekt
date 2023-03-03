import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Post } from '../_models/post'

@Injectable({
  providedIn: 'root'
})
export class PostService {

  private readonly apiUrl = environment.apiUrl + 'post'

  constructor(private http: HttpClient) { }


  // henter ALLE posts
  getAll(): Observable<Post[]>{
    return this.http.get<Post[]>(this.apiUrl)
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
  deletePost(postId: number){
    return this.http.delete<Post>(`${this.apiUrl}/${postId}`)
  }

  //update post
  editPost(post: Post){
    return this.http.put<Post>(`${this.apiUrl}/${post.postId}`, post)
  }

}
