import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { LoginRequest, LoginResponse } from '../../Model/auth.models';

@Injectable({
  providedIn: 'root',
})
export class Auth {

   private http = inject(HttpClient);
   private apiUrl = `${environment.apiUrl}/auth/login`;

  login(email: string, password: string): Observable<LoginResponse> {
    const request: LoginRequest = {
      API_Action: 'GetLoginData',
      Device_Id: 'D001',
      Sync_Time: '',
      Company_Code: email,  
      API_Body: {
        Username: email,
        Pw: password
      }
    };

    return this.http.post<LoginResponse>(this.apiUrl, request);
}
}
