import { Login } from "./login";
import { Post } from "./post";

export interface User {
    userId: number;
    // userName: string;
    firstName: string;
    lastName: string;
    address: string;
    created?: Date;
    login?: Login;
    posts?: Post[];
}


// export interface User {
//     userId: number;
//     userName: string;
//     created?: Date;
//     login?: Login;
//     posts?: Post[];
// }