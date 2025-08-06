import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { VideoService } from '../../services/video.service';
import { AuthService } from '../../services/auth.service';
import { Video } from '../../models/video.model';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-video-feed',
  templateUrl: './video-feed.component.html',
  styleUrls: ['./video-feed.component.scss']
})
export class VideoFeedComponent implements OnInit {
  @ViewChild('videoContainer') videoContainer!: ElementRef;
  
  videos: Video[] = [];
  currentVideoIndex = 0;
  loading = false;
  currentUser: User | null = null;
  showComments = false;
  isPlaying = true;

  // Demo data for showcase
  demoVideos: Video[] = [
    {
      id: '1',
      title: 'Amazing Dance Moves! 💃',
      description: 'Check out this incredible dance routine! #dance #viral #fyp',
      videoUrl: '/assets/demo-video1.mp4',
      thumbnailUrl: '/assets/thumb1.jpg',
      duration: 30,
      viewsCount: 125000,
      likesCount: 8500,
      commentsCount: 234,
      sharesCount: 89,
      isLiked: false,
      hashtags: ['dance', 'viral', 'fyp'],
      createdAt: new Date(),
      updatedAt: new Date(),
      user: {
        id: '1',
        username: 'dancer_queen',
        email: 'dancer@example.com',
        fullName: 'Sarah Johnson',
        profilePicture: 'https://images.pexels.com/photos/774909/pexels-photo-774909.jpeg?auto=compress&cs=tinysrgb&w=100&h=100&fit=crop',
        bio: 'Professional dancer 💃',
        followersCount: 45000,
        followingCount: 123,
        videosCount: 89,
        isFollowing: false,
        createdAt: new Date()
      }
    },
    {
      id: '2',
      title: 'Cooking Hack! 🍳',
      description: 'This will change how you cook forever! #cooking #lifehack #food',
      videoUrl: '/assets/demo-video2.mp4',
      thumbnailUrl: '/assets/thumb2.jpg',
      duration: 45,
      viewsCount: 89000,
      likesCount: 6200,
      commentsCount: 156,
      sharesCount: 234,
      isLiked: true,
      hashtags: ['cooking', 'lifehack', 'food'],
      createdAt: new Date(),
      updatedAt: new Date(),
      user: {
        id: '2',
        username: 'chef_master',
        email: 'chef@example.com',
        fullName: 'Mike Chen',
        profilePicture: 'https://images.pexels.com/photos/1222271/pexels-photo-1222271.jpeg?auto=compress&cs=tinysrgb&w=100&h=100&fit=crop',
        bio: 'Food enthusiast 👨‍🍳',
        followersCount: 78000,
        followingCount: 234,
        videosCount: 156,
        isFollowing: true,
        createdAt: new Date()
      }
    }
  ];

  constructor(
    private videoService: VideoService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
    });
    
    // Use demo data for showcase, replace with real API call
    this.videos = this.demoVideos;
    // this.loadVideos();
  }

  loadVideos() {
    this.loading = true;
    this.videoService.getTrendingVideos().subscribe({
      next: (videos) => {
        // Map backend video format to frontend format
        this.videos = videos.map(video => ({
          ...video,
          sharesCount: 0, // Backend doesn't have shares yet
          isLiked: false, // Will be determined by backend
          hashtags: [], // Will be populated from backend
          user: {
            ...video.user,
            profilePicture: video.user.avatarUrl || 'https://images.pexels.com/photos/771742/pexels-photo-771742.jpeg?auto=compress&cs=tinysrgb&w=100&h=100&fit=crop',
            username: video.user.username || video.user.fullName,
            bio: '',
            followersCount: 0,
            followingCount: 0,
            videosCount: 0,
            isFollowing: false,
            createdAt: new Date()
          }
        }));
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading videos:', error);
        this.loading = false;
        // Fallback to demo data
        this.videos = this.demoVideos;
      }
    });
  }

  getCurrentVideo(): Video | null {
    return this.videos[this.currentVideoIndex] || null;
  }

  nextVideo() {
    if (this.currentVideoIndex < this.videos.length - 1) {
      this.currentVideoIndex++;
      this.isPlaying = true;
    }
  }

  previousVideo() {
    if (this.currentVideoIndex > 0) {
      this.currentVideoIndex--;
      this.isPlaying = true;
    }
  }

  togglePlayPause() {
    this.isPlaying = !this.isPlaying;
  }

  onLike(video: Video) {
    if (video.isLiked) {
      this.videoService.unlikeVideo(video.id).subscribe({
        next: () => {
          video.isLiked = false;
          video.likesCount--;
        },
        error: (error) => console.error('Error unliking video:', error)
      });
    } else {
      this.videoService.likeVideo(video.id).subscribe({
        next: () => {
          video.isLiked = true;
          video.likesCount++;
        },
        error: (error) => console.error('Error liking video:', error)
      });
    }
  }

  onShare(video: Video) {
    if (navigator.share) {
      navigator.share({
        title: video.title,
        text: video.description || video.title,
        url: window.location.href
      });
    } else {
      // Fallback for browsers without Web Share API
      navigator.clipboard.writeText(window.location.href).then(() => {
        // Could show a toast notification here
        console.log('Link copied to clipboard');
      });
    }
  }

  toggleComments() {
    this.showComments = !this.showComments;
  }

  formatCount(count: number): string {
    if (count >= 1000000) {
      return (count / 1000000).toFixed(1) + 'M';
    } else if (count >= 1000) {
      return (count / 1000).toFixed(1) + 'K';
    }
    return count.toString();
  }
}