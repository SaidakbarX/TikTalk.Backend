import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { User } from '../../models/user.model';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {
  currentRoute = '';
  currentUser: User | null = null;

  constructor(
    private router: Router,
    private authService: AuthService
  ) {}

  ngOnInit() {
    // Track current route
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event: NavigationEnd) => {
      this.currentRoute = event.url;
    });

    // Get current user
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
    });
  }

  navigateToFeed() {
    this.router.navigate(['/feed']);
  }

  navigateToUpload() {
    this.router.navigate(['/upload']);
  }

  navigateToProfile() {
    if (this.currentUser) {
      this.router.navigate(['/profile']);
    }
  }

  logout() {
    this.authService.logout().subscribe(() => {
      this.router.navigate(['/auth']);
    });
  }

  isActive(route: string): boolean {
    return this.currentRoute.includes(route);
  }
}