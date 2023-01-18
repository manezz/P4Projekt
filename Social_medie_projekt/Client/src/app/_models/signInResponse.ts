import { Role } from "./role";

export interface SignInResponse {
    token: string;
    loginResponse: LoginResponse
}

export interface LoginResponse {
    loginId: number;
    email: string;
    type: Role;
    customer: CustomerResponse;
}

export interface CustomerResponse {
    customerId: number;
    firstName: string;
    lastName: string;
    address: string;
    created: Date;
}