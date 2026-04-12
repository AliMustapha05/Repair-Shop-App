import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { firstValueFrom } from 'rxjs';

import { RepairsService } from '../../../../core/services/repair.service';
import { DeviceService } from '../../../../core/services/device.service';
import { StatusStepsService } from '../../../../core/services/status-step.service';

import { DeviceDto } from '../../../../shared/models/device.model';
import { StatusStepDto } from '../../../../shared/models/status-step.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  repairs: any[] = [];
  devices: DeviceDto[] = [];
  statuses: StatusStepDto[] = [];
  activities: any[] = [];

  inProgressCount = 0;
  readyCount = 0;
  waitingCount = 0;
  diagnosedCount = 0;

  isLoaded = false;

  constructor(
    private repairsService: RepairsService,
    private deviceService: DeviceService,
    private statusService: StatusStepsService,
    private cdr: ChangeDetectorRef   // ✅ FIX ADDED
  ) {}

  ngOnInit(): void {
    console.log('Dashboard loaded');
    this.loadData();
  }

  async loadData() {

    this.isLoaded = false;

    console.log('Loading dashboard...');

    try {

      const [repairs, devices, statuses] = await Promise.all([
        firstValueFrom(this.repairsService.getAll()),
        firstValueFrom(this.deviceService.getAll()),
        firstValueFrom(this.statusService.getAll())
      ]);

      const activities = await firstValueFrom(
        this.repairsService.getLatestActivity()
      );

      this.devices = devices || [];
      this.statuses = statuses || [];
      this.activities = activities ?? [];

      this.repairs = (repairs || []).map((r: any) => ({
        ...r,
        deviceName: this.getDeviceName(r.deviceId),
        ownerName: this.getOwnerName(r.deviceId),
        statusName: this.getStatusName(r.currentStatusId)
      }));

      this.calculateStats();

      this.isLoaded = true;

      // ✅ FIX: force Angular UI refresh
      this.cdr.detectChanges();

      console.log('Dashboard data ready');
      console.log('Dashboard loaded state = TRUE');

    } catch (err) {

      console.error(err);

      this.isLoaded = true;
      this.cdr.detectChanges();
    }
  }

  calculateStats() {
    this.inProgressCount = this.repairs.filter(r => r.statusName === 'In Progress').length;
    this.readyCount = this.repairs.filter(r => r.statusName === 'Ready for Pickup').length;
    this.waitingCount = this.repairs.filter(r => r.statusName === 'Waiting for Parts').length;
    this.diagnosedCount = this.repairs.filter(r => r.statusName === 'Diagnosed').length;
  }

  getDeviceName(id: number): string {
    const d = this.devices.find(x => x.id === id);
    return d ? `${d.brand} ${d.model}` : 'Unknown';
  }

  getOwnerName(id: number): string {
    const d = this.devices.find(x => x.id === id);
    return d ? d.ownerName : '-';
  }

  getStatusName(id: number): string {
    const s = this.statuses.find(x => x.id === id);
    return s ? s.name : 'Unknown';
  }

  getDeviceIcon(deviceId: number): string {
    const d = this.devices.find(x => x.id === deviceId);

    switch (d?.deviceTypeId) {
      case 1: return '📱';
      case 2: return '💻';
      case 3: return '📲';
      case 4: return '🖥️';
      default: return '📦';
    }
  }

  getStatusClass(name: string): string {
    switch (name) {
      case 'Ready for Pickup':
        return 'badge green';
      case 'In Progress':
        return 'badge orange';
      case 'Waiting for Parts':
        return 'badge yellow';
      case 'Diagnosed':
        return 'badge blue';
      case 'Cancelled':
        return 'badge red';
      default:
        return 'badge gray';
    }
  }
}