import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-user-profile',
  template: `
    <div class="profile-container" *ngIf="currentUser">
      <div class="profile-header">
        <img [src]="currentUser.profilePicture || 'https://images.pexels.com/photos/771742/pexels-photo-771742.jpeg?auto=compress&cs=tinysrgb&w=200&h=200&fit=crop'" 
             [alt]="currentUser.fullName"
             class="profile-avatar">
        <h2>{{ currentUser.fullName }}</h2>
        <p>@{{ currentUser.username || currentUser.email.split('@')[0] }}</p>
        <div class="profile-stats">
          <div class="stat-item">
            <div class="stat-number">{{ currentUser.followingCount || 0 }}</div>
            <div class="stat-label">Following</div>
          </div>
          <div class="stat-item">
            <div class="stat-number">{{ currentUser.followersCount || 0 }}</div>
            <div class="stat-label">Followers</div>
          </div>
          <div class="stat-item">
            <div class="stat-number">{{ currentUser.videosCount || 0 }}</div>
            <div class="stat-label">Videos</div>
          </div>
        </div>
        <button class="auth-btn" (click)="logout()" style="margin-top: 20px;">Logout</button>
      </div>
    </div>
  `,
  styles: []
})
export class UserProfileComponent implements OnInit {
  currentUser: User | null = null;

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
    });
  }

  logout() {
    this.authService.logout().subscribe();
  }
}