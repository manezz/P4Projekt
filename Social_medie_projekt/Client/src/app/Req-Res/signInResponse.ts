import { Role } from "../_models/role";

export interface SignInResponse {
    token: string;
    loginResponse: LoginResponse
}

export interface LoginResponse {
    loginId: number;
    email: string;
    type: Role;
    user: userResponse;
}

export interface userResponse {
    userId: number;
    userName: string;
    created: Date;
    posts: postResponse;
}

export interface postResponse{
    postId: number;
    userId: number;
    title: string;
    desc?: string;
    tags?: string;
    pictureURL?: string;
    likes: number;
    date: Date;
}