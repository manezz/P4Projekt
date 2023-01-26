import { Login } from "./login";

export interface User {
    userId: number;
    username: string;
    firstName: string;
    lastName: string;
    address: string;
    created?: Date;
    login: Login;
}