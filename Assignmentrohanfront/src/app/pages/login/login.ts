import { Component , inject  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Auth } from '../../Service/Auth/auth';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { LoginResponse } from '../../Model/auth.models';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.html'
})
export class LoginComponent {
 private authService = inject(Auth);
  private toastr = inject(ToastrService);
  private router = inject(Router);

  email: string = '';
  password: string = '';
  showPassword: boolean = false;
  isLoading: boolean = false;

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  login (){

     // --- Validation ---
    if (!this.email.trim() || !this.password.trim()) {
      this.toastr.warning('Please enter both email and password.', 'Validation');
      return;
    }

    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailPattern.test(this.email)) {
      this.toastr.warning('Please enter a valid email address.', 'Validation');
      return;
    }


    this.authService.login(this.email, this.password).subscribe({
      next: (response: LoginResponse) => {
        this.isLoading = false;

        if (response.Status_Code === 200 && response.Response_Body?.length) {
          this.toastr.success(response.Message?.toString());
         // sessionStorage.setItem('user', JSON.stringify(response.Response_Body[0]));
          this.router.navigate(['/purchase-bill']);
        } else {
          this.toastr.error(response.Message?.toString());
        }
      },
      error: (err) => {
        this.isLoading = false;
        console.error('Login error:', err);
        this.toastr.error('Unable to reach the server. Please try again.', 'Error');
      }
    });
  }
  }
