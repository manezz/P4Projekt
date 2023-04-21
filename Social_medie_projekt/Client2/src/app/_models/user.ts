import { Follow } from './follow';
import { Login } from './login';
import { Post } from './post';
import { UserImage } from './userImage';

export interface User {
  userId?: number;
  userName?: string;
  created?: Date;
  login?: Login;
  posts?: Post[];
  userImage?: UserImage;
  follow?: Follow[];
}
