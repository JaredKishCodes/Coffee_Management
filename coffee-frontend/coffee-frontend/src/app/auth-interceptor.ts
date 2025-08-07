import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const authData = localStorage.getItem('auth');
  let token: string | null = null;
  if (authData) {
    try {
      token = JSON.parse(authData).token;
    } catch {
      token = null;
    }
  }

  if (token) {
    const AuthReq = req.clone({
      setHeaders: { Authorization: `Bearer ${token}` }
    });
    return next(AuthReq);
  }
  return next(req);
};