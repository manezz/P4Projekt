import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Post } from '../_models/post'

@Injectable({
  providedIn: 'root'
})
export class PostService {

  private readonly apiUrl = environment.apiUrl + 'Post' ;

  constructor(private http: HttpClient) { }


  //getall every posts
  getAll(){
    return this.http.get<Post[]>(`${this.apiUrl}`)
  }

  //getall own posts
  getAllSelf(Id: number){
    return this.http.get<Post[]>(`${this.apiUrl}/${Id}`)
  }

  //create post
  createPost(post: Post){
    return this.http.post<Post>(this.apiUrl, post)
  }

  //delete post
  delPost(){

  }

  //update post
  updPost(){

  }

}
