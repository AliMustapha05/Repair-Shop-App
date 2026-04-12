import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { DeviceService } from '../../../../core/services/device.service';
import { DeviceTypesService } from '../../../../core/services/device-type.service';

import { DeviceDto } from '../../../../shared/models/device.model';
import { DeviceTypeDto } from '../../../../shared/models/device-type.model';

import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';

@Component({
  selector: 'app-device-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './device-list.component.html',
  styleUrls: ['./device-list.component.css']
})
export class DeviceListComponent implements OnInit {

  devices: DeviceDto[] = [];
  deviceTypes: DeviceTypeDto[] = [];

  constructor(
    private deviceService: DeviceService,
    private deviceTypeService: DeviceTypesService,
    private loader: LoaderService,
    private toast: ToastService
  ) {}

  ngOnInit(): void {
    this.loadTypes();
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
        this.toast.error('Failed to load devices');
      }
    });
  }

  loadTypes() {
    this.deviceTypeService.getAll().subscribe(res => {
      this.deviceTypes = res;
    });
  }

  getTypeName(id: number): string {
    return this.deviceTypes.find(t => t.id === id)?.name || 'Unknown';
  }

  getDeviceIcon(typeId: number): string {
    return this.deviceTypes.find(t => t.id === typeId)?.icon || '📦';
  }
}