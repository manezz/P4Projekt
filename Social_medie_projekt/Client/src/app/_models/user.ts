import { Login } from "./login";

export interface User {
    id: number;
    loginId: number;
    firstname: string;
    lastname: string;
    address: string;
    createdate: string;
    login: Login;
}