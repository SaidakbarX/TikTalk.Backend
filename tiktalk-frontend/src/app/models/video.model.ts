import { User } from './user.model';

export interface Video {
  id: string;
  title: string;
  description: string;
  videoUrl: string;
  thumbnailUrl?: string;
  duration: number;
  viewsCount: number;
  likesCount: number;
  commentsCount: number;
  sharesCount: number;
  isLiked?: boolean;
  user: User;
  hashtags: string[];
  createdAt: Date;
  updatedAt: Date;
}

export interface VideoUploadRequest {
  title: string;
  description: string;
  hashtags: string[];
  videoFile: File;
}

export interface Comment {
  id: string;
  content: string;
  user: User;
  videoId: string;
  likesCount: number;
  isLiked?: boolean;
  createdAt: Date;
}

export interface VideoLike {
  id: string;
  userId: string;
  videoId: string;
  createdAt: Date;
}

export interface VideoAnalytics {
  videoId: string;
  totalViews: number;
  totalLikes: number;
  totalComments: number;
  totalShares: number;
  viewsByDay: { date: string; views: number }[];
  demographics: {
    ageGroups: { range: string; count: number }[];
    countries: { country: string; count: number }[];
  };
}