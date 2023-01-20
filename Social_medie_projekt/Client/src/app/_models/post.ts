import { Tag } from "./tags";
import { User } from "./user"

export interface Post {
    postId: number;
    userId: number;
    title: string;
    date: string;
    desc?: string;
    pictureURL?: string;
    

    // ??
    likedByUser: boolean;
    tags: Tag[];
    user: User;
}