import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router)
  const authData = localStorage.getItem('auth');

  // Check if auth exists and contains a token
  if (authData) {
    try {
      const parsed = JSON.parse(authData);
      if (parsed.token) return true;
    } catch {
      console.warn('Auth data is corrupted');
    }
  }

   return router.navigate(['/login']);
};
