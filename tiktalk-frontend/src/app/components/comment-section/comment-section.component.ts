import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-comment-section',
  template: `
    <div class="comment-section" *ngIf="isVisible">
      <div class="comment-header">
        <h3>Comments</h3>
        <button (click)="close.emit()" class="close-btn">
          <mat-icon>close</mat-icon>
        </button>
      </div>
      <div class="comment-list">
        <div class="comment-item">
          <div class="comment-avatar"></div>
          <div class="comment-content">
            <strong>user123</strong>
            <p>Amazing video! 🔥</p>
          </div>
        </div>
        <div class="comment-item">
          <div class="comment-avatar"></div>
          <div class="comment-content">
            <strong>dancer_girl</strong>
            <p>How did you do that move?</p>
          </div>
        </div>
      </div>
      <div class="comment-input">
        <input type="text" placeholder="Add a comment..." />
        <button class="send-btn">
          <mat-icon>send</mat-icon>
        </button>
      </div>
    </div>
  `,
  styles: [`
    .comment-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding-bottom: 15px;
      border-bottom: 1px solid rgba(255,255,255,0.1);
    }
    .close-btn {
      background: none;
      border: none;
      color: white;
      cursor: pointer;
    }
    .comment-list {
      flex: 1;
      overflow-y: auto;
      padding: 15px 0;
    }
    .comment-content {
      flex: 1;
    }
    .comment-content strong {
      color: white;
      font-size: 14px;
    }
    .comment-content p {
      color: rgba(255,255,255,0.8);
      font-size: 14px;
      margin: 4px 0 0 0;
    }
    .send-btn {
      background: #fe2c55;
      border: none;
      color: white;
      padding: 8px;
      border-radius: 50%;
      cursor: pointer;
    }
  `]
})
export class CommentSectionComponent {
  @Input() videoId!: string;
  @Input() isVisible = false;
  @Output() close = new EventEmitter<void>();
}