import { Component } from '@angular/core';

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { VideoService } from '../../services/video.service';
import { VideoUploadRequest } from '../../models/video.model';

@Component({
  selector: 'app-video-upload',
  template: `
    <div class="upload-container">
      <h2>Upload Your Video</h2>
      <div class="upload-area" (click)="fileInput.click()" (dragover)="onDragOver($event)" (drop)="onDrop($event)">
        <mat-icon style="font-size: 64px; color: #fe2c55;">cloud_upload</mat-icon>
        <h3>Drag and drop your video here</h3>
        <p>Or click to browse files</p>
        <button class="auth-btn" style="margin-top: 20px;" type="button">Choose File</button>
      </div>
      
      <input #fileInput type="file" accept="video/*" style="display: none;" (change)="onFileSelected($event)">
      
      <div *ngIf="selectedFile" class="file-info">
        <p>Selected: {{ selectedFile.name }}</p>
        <mat-form-field class="form-field" appearance="outline">
          <mat-label>Video Title</mat-label>
          <input matInput [(ngModel)]="videoTitle" placeholder="Enter video title">
        </mat-form-field>
        
        <mat-form-field class="form-field" appearance="outline">
          <mat-label>Description</mat-label>
          <textarea matInput [(ngModel)]="videoDescription" placeholder="Describe your video" rows="3"></textarea>
        </mat-form-field>
        
        <button class="auth-btn" (click)="uploadVideo()" [disabled]="uploading">
          <span *ngIf="!uploading">Upload Video</span>
          <mat-spinner *ngIf="uploading" diameter="20"></mat-spinner>
        </button>
      </div>
    </div>
  `,
  styles: [`
    .file-info {
      margin-top: 20px;
      width: 100%;
      max-width: 500px;
    }
    
    .file-info p {
      color: #25f4ee;
      margin-bottom: 20px;
      font-weight: 500;
    }
    
    .form-field {
      width: 100%;
      margin-bottom: 20px;
    }
    
    @media (min-width: 768px) {
      .file-info {
        max-width: 600px;
      }
    }
  `]
})
export class VideoUploadComponent {
  selectedFile: File | null = null;
  videoTitle = '';
  videoDescription = '';
  uploading = false;

  constructor(
    private videoService: VideoService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  onDragOver(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
  }

  onDrop(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    
    const files = event.dataTransfer?.files;
    if (files && files.length > 0) {
      this.handleFile(files[0]);
    }
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.handleFile(file);
    }
  }

  private handleFile(file: File) {
    if (file.type.startsWith('video/')) {
      this.selectedFile = file;
      // Auto-generate title from filename
      this.videoTitle = file.name.replace(/\.[^/.]+$/, "");
    } else {
      this.snackBar.open('Please select a video file', 'Close', { duration: 3000 });
    }
  }

  uploadVideo() {
    if (!this.selectedFile || !this.videoTitle.trim()) {
      this.snackBar.open('Please select a file and enter a title', 'Close', { duration: 3000 });
      return;
    }

    this.uploading = true;
    
    const uploadData: VideoUploadRequest = {
      title: this.videoTitle,
      description: this.videoDescription,
      hashtags: this.extractHashtags(this.videoDescription),
      videoFile: this.selectedFile
    };

    this.videoService.uploadVideo(uploadData).subscribe({
      next: (response) => {
        this.uploading = false;
        this.snackBar.open('Video uploaded successfully!', 'Close', { duration: 3000 });
        this.router.navigate(['/feed']);
      },
      error: (error) => {
        this.uploading = false;
        this.snackBar.open('Upload failed. Please try again.', 'Close', { duration: 3000 });
        console.error('Upload error:', error);
      }
    });
  }

  private extractHashtags(text: string): string[] {
    const hashtags = text.match(/#\w+/g);
    return hashtags ? hashtags.map(tag => tag.substring(1)) : [];
  }
}