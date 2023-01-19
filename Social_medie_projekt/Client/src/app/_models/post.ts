import { Tags } from "./tags";
import { User } from "./user"

export interface Post {
    postId: number;
    userId: number;
    title: string;
    date: string;
    desc?: string;
    pictureURL?: string;
    tag?: Array<string>;
    likes: number;

    // ??
    likedByUser: boolean;
    tag: Tags;
    user: User;
}