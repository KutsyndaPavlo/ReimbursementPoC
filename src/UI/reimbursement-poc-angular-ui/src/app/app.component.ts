import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet , RouterLinkActive} from '@angular/router';
import { ApiAuthorizationModule } from './../../src/api-authorization/api-authorization.module';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLinkActive, ApiAuthorizationModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'reimbursement-poc-angular-ui';
}
