import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';

import { AuthComponent } from './components/auth/auth.component';
import { VideoFeedComponent } from './components/video-feed/video-feed.component';
import { VideoUploadComponent } from './components/video-upload/video-upload.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';

const routes: Routes = [
  { path: '', redirectTo: '/feed', pathMatch: 'full' },
  { path: 'auth', component: AuthComponent },
  { path: 'feed', component: VideoFeedComponent, canActivate: [AuthGuard] },
  { path: 'upload', component: VideoUploadComponent, canActivate: [AuthGuard] },
  { path: 'profile/:id', component: UserProfileComponent, canActivate: [AuthGuard] },
  { path: 'profile', component: UserProfileComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '/feed' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }