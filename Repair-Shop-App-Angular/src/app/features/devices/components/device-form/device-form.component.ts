import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-device-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './device-form.component.html',
  styleUrls: ['./device-form.component.css']
})
export class DeviceFormComponent {

  device = {
    name: '',
    serialNumber: '',
    deviceTypeId: 0
  };

  submit() {
    console.log(this.device); // later we connect to API
  }

}