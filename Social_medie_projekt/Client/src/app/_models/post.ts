import { Tags } from "./tags";
import { User } from "./user"

export interface Post {
    postId: number;
    userId: number;
    title: string;
    desc?: string;
    pictureURL?: string;
    likes: number;
    date: string;

    // ??
    likedByUser: boolean;
    tag: Tags;
    user: User;
}