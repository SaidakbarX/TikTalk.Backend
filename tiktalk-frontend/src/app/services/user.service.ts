import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = 'https://localhost:7017/api/users';

  constructor(private http: HttpClient) {}

  getUserById(id: string): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}/${id}`);
  }

  updateProfile(updates: Partial<User>): Observable<User> {
    return this.http.put<User>(`${this.baseUrl}/profile`, updates);
  }

  followUser(userId: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/${userId}/follow`, {});
  }

  unfollowUser(userId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${userId}/follow`);
  }

  getFollowers(userId: string, page: number = 1, limit: number = 20): Observable<User[]> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('limit', limit.toString());
    
    return this.http.get<User[]>(`${this.baseUrl}/${userId}/followers`, { params });
  }

  getFollowing(userId: string, page: number = 1, limit: number = 20): Observable<User[]> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('limit', limit.toString());
    
    return this.http.get<User[]>(`${this.baseUrl}/${userId}/following`, { params });
  }

  searchUsers(query: string, page: number = 1, limit: number = 10): Observable<User[]> {
    const params = new HttpParams()
      .set('q', query)
      .set('page', page.toString())
      .set('limit', limit.toString());
    
    return this.http.get<User[]>(`${this.baseUrl}/search`, { params });
  }

  uploadProfilePicture(file: File): Observable<{ profilePicture: string }> {
    const formData = new FormData();
    formData.append('profilePicture', file);
    
    return this.http.post<{ profilePicture: string }>(`${this.baseUrl}/profile/picture`, formData);
  }

  getSuggestedUsers(limit: number = 10): Observable<User[]> {
    const params = new HttpParams().set('limit', limit.toString());
    return this.http.get<User[]>(`${this.baseUrl}/suggested`, { params });
  }

  blockUser(userId: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/${userId}/block`, {});
  }

  unblockUser(userId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${userId}/block`);
  }

  reportUser(userId: string, reason: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/${userId}/report`, { reason });
  }
}