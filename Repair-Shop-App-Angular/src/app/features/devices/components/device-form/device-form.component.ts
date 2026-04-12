import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

import { DeviceService } from '../../../../core/services/device.service';
import { ToastService } from '../../../../shared/services/toast.service';
import { CreateDeviceDto, DeviceDto } from '../../../../shared/models/device.model';

@Component({
  selector: 'app-device-form',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './device-form.component.html',
  styleUrls: ['./device-form.component.css']
})
export class DeviceFormComponent implements OnInit {

  deviceId: number | null = null;

  // IMPORTANT: always use CreateDeviceDto for form
  device: CreateDeviceDto = {
    brand: '',
    model: '',
    serialNumber: '',
    ownerName: '',
    ownerPhone: '',
    deviceTypeId: 0
  };

  constructor(
    private deviceService: DeviceService,
    private route: ActivatedRoute,
    private router: Router,
    private toast: ToastService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.deviceId = +id;
      this.loadDevice(this.deviceId);
    }
  }

  loadDevice(id: number) {
    this.deviceService.getById(id).subscribe({
      next: (res: DeviceDto) => {
        this.device = {
          brand: res.brand,
          model: res.model,
          serialNumber: res.serialNumber,
          ownerName: res.ownerName,
          ownerPhone: res.ownerPhone,
          deviceTypeId: res.deviceTypeId
        };
      },
      error: () => {
        this.toast.error('Failed to load device');
      }
    });
  }

  save() {
    // ================= CREATE =================
    if (!this.deviceId) {
      this.deviceService.create(this.device).subscribe({
        next: () => {
          this.toast.success('Device created successfully');
          this.router.navigate(['/devices']);
        },
        error: (err) => {
          console.log(err);
          this.toast.error('Create failed');
        }
      });
      return;
    }

    // ================= UPDATE =================
    this.deviceService.update(this.deviceId, {
      id: this.deviceId,
      ...this.device
    }).subscribe({
      next: () => {
        this.toast.success('Device updated successfully');
        this.router.navigate(['/devices']);
      },
      error: (err) => {
        console.log(err);
        this.toast.error('Update failed');
      }
    });
  }
}