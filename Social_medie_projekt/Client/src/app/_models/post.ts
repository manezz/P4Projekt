import { Likes } from "./likes";
import { Tags } from "./tags";
import { User } from "./user"

export interface Post {
    userId?: number;
    postId: number;
    title: string;
    desc?: string;
    tags?: string;
    likes?: number;
    date?: Date;
    user?: User;
}