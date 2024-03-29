import { PostLikes } from './postLikes';
import { Tag } from './tag';
import { User } from './user';

export interface Post {
  userId?: number;
  postId: number;
  title: string;
  desc: string;
  likeUserId?: number;
  tags?: Tag[];
  postLikes?: PostLikes;
  date?: Date;
  user?: User;
}
