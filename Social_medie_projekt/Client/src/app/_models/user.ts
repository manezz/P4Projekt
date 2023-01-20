import { Login } from "./login";

export interface User {
    userId: number;
    firstName: string;
    lastName: string;
    address: string;
    created?: Date;
    login: Login;
}