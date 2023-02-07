import { Login } from "./login";
import { Post } from "./post";

export interface User {
    userId: number;
    loginId: number;
    firstName: string;
    lastName: string;
    address: string;
    created: Date;
    login: Login;
    posts?: Post[];
}