import { PostLikes } from './postLikes';
import { Tag } from './tags';
import { User } from './user';

export interface Post {
  userId?: number;
  postId: number;
  title: string;
  desc: string;
  tags?: string;
  postLikes?: PostLikes;
  date?: Date;
  user?: User;
}
