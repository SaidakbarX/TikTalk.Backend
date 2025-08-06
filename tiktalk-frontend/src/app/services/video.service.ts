import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Video, VideoUploadRequest, Comment, VideoAnalytics } from '../models/video.model';

@Injectable({
  providedIn: 'root'
})
export class VideoService {
  private baseUrl = 'https://localhost:7000/api/videos';

  constructor(private http: HttpClient) {}

  getTrendingVideos(page: number = 1, limit: number = 10): Observable<Video[]> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('limit', limit.toString());
    
    return this.http.get<Video[]>(this.baseUrl, { params });
  }

  getVideoById(id: string): Observable<Video> {
    return this.http.get<Video>(`${this.baseUrl}/${id}`);
  }

  uploadVideo(videoData: VideoUploadRequest): Observable<Video> {
    const formData = new FormData();
    formData.append('title', videoData.title);
    formData.append('description', videoData.description);
    formData.append('hashtags', videoData.hashtags.join(','));
    formData.append('video', videoData.videoFile);

    return this.http.post<Video>(this.baseUrl, formData);
  }

  likeVideo(videoId: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/${videoId}/like`, {});
  }

  unlikeVideo(videoId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${videoId}/like`);
  }

  getVideoComments(videoId: string, page: number = 1, limit: number = 20): Observable<Comment[]> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('limit', limit.toString());
    
    return this.http.get<Comment[]>(`${this.baseUrl}/${videoId}/comments`, { params });
  }

  addComment(videoId: string, content: string): Observable<Comment> {
    return this.http.post<Comment>(`${this.baseUrl}/${videoId}/comments`, { content });
  }

  likeComment(commentId: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/comments/${commentId}/like`, {});
  }

  unlikeComment(commentId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/comments/${commentId}/like`);
  }

  getVideoAnalytics(videoId: string): Observable<VideoAnalytics> {
    return this.http.get<VideoAnalytics>(`${this.baseUrl}/${videoId}/analytics`);
  }

  getUserVideos(userId: string, page: number = 1, limit: number = 12): Observable<Video[]> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('limit', limit.toString());
    
    return this.http.get<Video[]>(`/api/users/${userId}/videos`, { params });
  }

  searchVideos(query: string, page: number = 1, limit: number = 10): Observable<Video[]> {
    const params = new HttpParams()
      .set('q', query)
      .set('page', page.toString())
      .set('limit', limit.toString());
    
    return this.http.get<Video[]>(`${this.baseUrl}/search`, { params });
  }

  deleteVideo(videoId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${videoId}`);
  }

  updateVideo(videoId: string, updates: Partial<Video>): Observable<Video> {
    return this.http.put<Video>(`${this.baseUrl}/${videoId}`, updates);
  }
}