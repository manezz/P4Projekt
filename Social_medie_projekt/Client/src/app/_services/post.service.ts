import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Post } from '../_models/post';
import { Tag } from '../_models/tags';
import { PostPageComponent } from '../postpage/postpage.component';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  private readonly apiUrl = environment.apiUrl + 'post';

  constructor(private http: HttpClient) {}


  //getall every posts
  getAll(): Observable<Post[]>{
    return this.http.get<Post[]>(this.apiUrl);
  }

  //getall own posts
  getAllSelf(){

  }

  //create post
  createPost(post: Post): Observable<Post>{
    console.log(post);
    return this.http.post<Post>(this.apiUrl, post);
  }

  //delete post
  delPost(){

  }

  //update post
  updPost(){

  }

}
