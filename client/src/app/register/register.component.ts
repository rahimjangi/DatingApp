import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  accountService = inject(AccountService);
  cancelRegister = output<boolean>();
  model: any = {};
  private toaster = inject(ToastrService);

  register() {
    console.log(this.model);
    this.accountService.register(this.model).subscribe({
      next: (response) => {
        console.log('register response', response);
        this.cancel();
      },
      error: (err) => this.toaster.error(err.error),
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
