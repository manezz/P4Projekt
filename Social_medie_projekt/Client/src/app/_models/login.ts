import { User } from './user';
import { Role } from './role';

export interface Login {
  loginId: number;
  email: string;
  password: string;
  role?: Role;
  user?: User;
  token?: string;
}

// export { Role };
