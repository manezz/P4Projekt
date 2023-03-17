import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Like } from '../_models/like';

@Injectable({
  providedIn: 'root',
})
export class LikeService {
  private readonly apiUrl = environment.apiUrl + 'Like';

  constructor(private http: HttpClient) {}

  postLike(like: Like) {
    return this.http.post<Like>(`${this.apiUrl}`, like);
  }

  deleteLike(userId: number, postId: number) {
    return this.http.delete<Like>(`${this.apiUrl}/${userId}/${postId}`);
  }
}
