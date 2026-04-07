import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DeviceService } from '../../../../core/services/device.service';
import { DeviceDto } from '../../../../shared/models/device.model';

import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';
import { EmptyStateComponent } from '../../../../shared/components/empty-state/empty-state.component';

@Component({
  selector: 'app-device-list',
  standalone: true,
  imports: [CommonModule, EmptyStateComponent],
  templateUrl: './device-list.component.html',
  styleUrls: ['./device-list.component.css']
})
export class DeviceListComponent implements OnInit {

  devices: DeviceDto[] = [];

  constructor(
    private deviceService: DeviceService,
    private loader: LoaderService,
    private toast: ToastService
  ) {}

  ngOnInit(): void {
    this.loadDevices();
  }

  loadDevices() {
    this.loader.show();

    this.deviceService.getAll().subscribe({
      next: (res) => {
        this.devices = res;
        this.loader.hide();
      },
      error: () => {
        this.loader.hide();
        this.toast.showError('Failed to load devices');
      }
    });
  }

  delete(id: number) {
    if (!confirm('Are you sure?')) return;

    this.loader.show();

    this.deviceService.delete(id).subscribe({
      next: () => {
        this.toast.showSuccess('Device deleted');
        this.loadDevices();
      },
      error: () => {
        this.loader.hide();
        this.toast.showError('Delete failed');
      }
    });
  }

}