import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const adminGuard : CanActivateFn = (route, state) => {
   const router = inject(Router)
   let role = '';

   
   const authData = localStorage.getItem('auth')
   if(authData){
      const authObj = JSON.parse(authData)
      role = authObj.role
      console.log(role)
   }

   if(role == 'Admin'){
    return true;
   }
   else{
    router.navigate(['/']); // Redirect to home if not admin
    return false;
   }

};
