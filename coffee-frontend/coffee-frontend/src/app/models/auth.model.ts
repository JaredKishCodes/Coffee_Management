export interface ILogin{
    email: string;
    password: string;
}

export interface IRegister{
    fullName:string;
    email:string;
    password:string
    confirmPassword:string;
}

export interface IAuthResponse {
  success: boolean;
  message: string;
  cartId : string;
  token: string;
  role: string,
  email: string;
  fullName: string;
}
