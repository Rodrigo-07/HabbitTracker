
interface DecodedUser {
    exp: number;
    iat: number;
    roles: string[];
    sub: string;

}
  

interface AuthResponse {
token: string;
}

export type { DecodedUser, AuthResponse };