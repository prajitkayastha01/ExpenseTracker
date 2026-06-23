// shared/guards/auth.guard.ts
import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const token = localStorage.getItem('token'); 

  if (token) {
    return true;
  }

  // No token found! Block navigation and redirect immediately to the login path
  return router.createUrlTree(['/login']);
};