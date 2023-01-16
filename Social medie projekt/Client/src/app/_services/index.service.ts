import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class IndexService {

  private readonly apiUrl = environment.apiUrl + '<INSERT URL HERE>';

  constructor(private http: HttpClient) { }


  //getall every posts

  //getall own posts

  //create post

  //delete post

  //update post

  //finduser

}
