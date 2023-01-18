import { User } from "./user";

export interface Login {
    id: number;
    email: string;
    password: string;
    roletype: number;
    user: User;
}