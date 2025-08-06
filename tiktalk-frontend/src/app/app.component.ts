import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  template: `
    <div class="container">
      <router-outlet></router-outlet>
      <app-navigation *ngIf="showNavigation"></app-navigation>
    </div>
  `,
  styles: []
})
export class AppComponent implements OnInit {
  showNavigation = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.authService.currentUser$.subscribe(user => {
      this.showNavigation = !!user;
      if (!user && this.router.url !== '/auth') {
        this.router.navigate(['/auth']);
      }
    });
  }
}