import {Role} from './role';

export class User {
  name: string;
  email: string;
  password: string;
  token?: string;
  role?: Role;
}
