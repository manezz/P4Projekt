import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Post } from '../_models/post'
import { DataService } from '../_services/data.service';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  private readonly apiUrl = environment.apiUrl + 'Post' ;

  constructor(private http: HttpClient, private data: DataService) { }


  //getall every posts
  getAll(): Observable<Post[]>{
    return this.http.get<Post[]>(`${this.apiUrl}`)
    // return this.data.getTempData()
  }

  //getall own posts
  getAllSelf(Id: number): Observable<Post[]>{
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
