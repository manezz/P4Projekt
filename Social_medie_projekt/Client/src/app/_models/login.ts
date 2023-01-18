import { User } from "./user";
import { Role } from "./role";

export interface Login {
    loginId: number;
    email: string;
    password: string;
    type?: Role;
    token?: string;
}