import { User } from "./user"

export interface Post {
    postId: number;
    // userId: number;
    user: User;
    title: string;
    date: string;
    desc?: string;
    pictureURL?: string;
    tag?: Array<string>;
    likedByUser: boolean;
}