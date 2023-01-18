export interface User {
    userId: number;
    username: string;
    profilePicURL: string;
    bio: string;
    followers?: Array<string>;
    following?: Array<string>;
}