import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { User, LoginRequest, RegisterRequest, AuthResponse } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'https://localhost:7017/api/auth'; // Updated backend URL
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) {
    this.loadUserFromStorage();
  }

  private loadUserFromStorage(): void {
    const token = localStorage.getItem('token');
    const userData = localStorage.getItem('user');
    
    if (token && userData) {
      try {
        const user = JSON.parse(userData);
        this.currentUserSubject.next(user);
      } catch (error) {
        console.error('Error parsing user data:', error);
        this.logout();
      }
    }
  }

  login(credentials: LoginRequest): Observable<AuthResponse> {
    // Backend expects UserLoginDto format
    const loginData = {
      provider: 'email',
      providerKey: credentials.email,
      password: credentials.password,
      loginTime: new Date()
    };
    
    return this.http.post<AuthResponse>(`${this.baseUrl}/login`, loginData)
      .pipe(
        tap(response => {
          localStorage.setItem('token', response.accessToken);
          localStorage.setItem('refreshToken', response.refreshToken);
          // Create user object from response
          const user = {
            id: '1', // Will be updated when backend returns user info
            username: credentials.email.split('@')[0],
            email: credentials.email,
            fullName: credentials.email.split('@')[0],
            profilePicture: '',
            bio: '',
            followersCount: 0,
            followingCount: 0,
            videosCount: 0,
            createdAt: new Date()
          };
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSubject.next(user);
        })
      );
  }

  register(userData: RegisterRequest): Observable<AuthResponse> {
    // Backend expects CreateUserDto format
    const signupData = {
      fullName: userData.fullName,
      email: userData.email,
      password: userData.password,
      username: userData.username,
      passwordHasher: '' // Will be handled by backend
    };
    
    return this.http.post<any>(`${this.baseUrl}/signup`, signupData)
      .pipe(
        tap(response => {
          // After successful signup, login the user
          this.login({ email: userData.email, password: userData.password }).subscribe();
        })
      );
  }

  logout(): Observable<any> {
    const refreshToken = localStorage.getItem('refreshToken');
    return this.http.post(`${this.baseUrl}/logout`, { token: refreshToken })
      .pipe(
        tap(() => {
          localStorage.removeItem('token');
          localStorage.removeItem('refreshToken');
          localStorage.removeItem('user');
          this.currentUserSubject.next(null);
        })
      );
  }

  refreshToken(): Observable<AuthResponse> {
    const refreshToken = localStorage.getItem('refreshToken');
    return this.http.post<AuthResponse>(`${this.baseUrl}/refresh`, { token: refreshToken })
      .pipe(
        tap(response => {
          localStorage.setItem('token', response.accessToken);
          localStorage.setItem('refreshToken', response.refreshToken);
          // Keep existing user data
          const currentUser = this.getCurrentUser();
          if (currentUser) {
            this.currentUserSubject.next(currentUser);
          }
        })
      );
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    if (!token) return false;
    
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.exp > Date.now() / 1000;
    } catch {
      return false;
    }
  }

  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }
}