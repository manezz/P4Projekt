import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Follow } from '../_models/follow';

@Injectable({
  providedIn: 'root',
})
export class FollowService {
  private readonly apiUrl = environment.apiUrl + 'Follow';

  constructor(private http: HttpClient) {}

  follow(follow: Follow) {
    return this.http.post<Follow>(`${this.apiUrl}/follow`, follow);
  }

  unFollow(userId: number, followingId: number) {
    return this.http.delete<Follow>(`${this.apiUrl}/unfollow/${userId}/${followingId}`);
  }




  getFollow(userId: number, followingId: any){
    return this.http.get<Follow>(`${this.apiUrl}/${userId}/${followingId}`);
  }

  findFollowers(userId: number){
    return this.http.get<Follow[]>(`${this.apiUrl}/${userId}`);
  }

  findFollowing(followingId: number){
    return this.http.get<Follow[]>(`${this.apiUrl}/${followingId}`);
  }




}
