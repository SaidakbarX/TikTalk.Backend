import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-user-profile',
  template: `
    <div class="profile-container" *ngIf="currentUser">
      <div class="profile-header">
        <img [src]="currentUser.profilePicture || '/assets/default-avatar.png'" 
             [alt]="currentUser.fullName"
             class="profile-avatar">
        <h2>{{ currentUser.fullName }}</h2>
        <p>@{{ currentUser.username }}</p>
        <div class="profile-stats">
          <div class="stat-item">
            <div class="stat-number">{{ currentUser.followingCount }}</div>
            <div class="stat-label">Following</div>
          </div>
          <div class="stat-item">
            <div class="stat-number">{{ currentUser.followersCount }}</div>
            <div class="stat-label">Followers</div>
          </div>
          <div class="stat-item">
            <div class="stat-number">{{ currentUser.videosCount }}</div>
            <div class="stat-label">Videos</div>
          </div>
        </div>
        <button class="auth-btn" (click)="logout()">Logout</button>
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