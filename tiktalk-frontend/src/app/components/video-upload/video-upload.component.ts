import { Component } from '@angular/core';

@Component({
  selector: 'app-video-upload',
  template: `
    <div class="upload-container">
      <h2>Upload Your Video</h2>
      <div class="upload-area">
        <mat-icon style="font-size: 64px; color: #fe2c55;">cloud_upload</mat-icon>
        <h3>Drag and drop your video here</h3>
        <p>Or click to browse files</p>
        <button class="auth-btn" style="margin-top: 20px;">Choose File</button>
      </div>
    </div>
  `,
  styles: []
})
export class VideoUploadComponent {}