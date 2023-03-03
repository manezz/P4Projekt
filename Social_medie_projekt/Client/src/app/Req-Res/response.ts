import { Role } from "../_models/role";

export interface LoginResponse {
    loginId: number
    email: string
    type: Role
    token: string
    user: userResponse
}

export interface userResponse {
    userId: number
    userName: string
    created: Date
    posts: postResponse
}

export interface postResponse{
    postId: number
    userId: number
    title: string
    desc?: string
    pictureURL?: string
    likes: number
    date: Date
    tags?: tagResponse
}

export interface tagResponse{
    tagId: number
    name: string
}