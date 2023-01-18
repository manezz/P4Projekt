import { User } from "./user"

export interface Post {
    postId: number;
    userId: number;
    title: string;
    date: string;
    desc?: string;
    pictureURL?: string;
    tag?: Array<string>;

    // ??
    likedByUser: boolean;

    user: User;
}