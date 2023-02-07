import { Likes } from "./likes";
import { Tags } from "./tags";
import { User } from "./user"

export interface Post {
    postId: number;
    title: string;
    desc?: string;
    likes: number;
    date: Date;
    user: User;
}