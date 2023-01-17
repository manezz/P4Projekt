import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  private readonly apiUrl = environment.apiUrl + '<INSERT URL HERE>';

  constructor(private http: HttpClient) { }


  //getall every posts

  //getall own posts

  //create post

  //delete post

  //update post

  //finduser

}
