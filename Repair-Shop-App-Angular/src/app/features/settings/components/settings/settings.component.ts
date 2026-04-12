import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { StatusStepsService } from '../../../../core/services/status-step.service';
import { DeviceTypesService } from '../../../../core/services/device-type.service';

import { StatusStepDto, CreateStatusStepDto } from '../../../../shared/models/status-step.model';
import { DeviceTypeDto, CreateDeviceTypeDto } from '../../../../shared/models/device-type.model';

import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  activeTab: 'status' | 'types' = 'status';

  // STATUS STEPS
  steps: StatusStepDto[] = [];
  newStepName = '';
  newTypeIcon: string = '📦';

  // DEVICE TYPES
  types: DeviceTypeDto[] = [];
  newTypeName = '';

  constructor(
    private statusService: StatusStepsService,
    private typeService: DeviceTypesService,
    private loader: LoaderService,
    private toast: ToastService
  ) {}

  ngOnInit(): void {
    this.loadAll();
  }

  // ---------------- LOAD ----------------
  loadAll() {
    this.loadStatusSteps();
    this.loadDeviceTypes();
  }

  loadStatusSteps() {
    this.statusService.getAll().subscribe({
      next: (res) => this.steps = res
    });
  }

  loadDeviceTypes() {
    this.typeService.getAll().subscribe({
      next: (res) => this.types = res
    });
  }

  // ---------------- STATUS STEPS ----------------
  addStatus() {
    if (!this.newStepName.trim()) return;

    const dto: CreateStatusStepDto = {
      name: this.newStepName
    };

    this.statusService.create(dto).subscribe({
      next: () => {
        this.toast.success('Status step added');
        this.newStepName = '';
        this.loadStatusSteps();
      },
      error: () => this.toast.error('Failed to add status step')
    });
  }

  deleteStatus(id: number) {
    if (!confirm('Delete this status step?')) return;

    this.statusService.delete(id).subscribe({
      next: () => {
        this.toast.success('Deleted');
        this.loadStatusSteps();
      },
      error: () => this.toast.error('Delete failed')
    });
  }

  // ---------------- DEVICE TYPES ----------------
  addType() {
    if (!this.newTypeName.trim()) return;

    const dto: any = {
      name: this.newTypeName,

      // ✅ ADD ICON SAFELY (NO BREAKING CHANGE)
      icon: this.newTypeIcon?.trim() ? this.newTypeIcon : '📦'
    };

    this.typeService.create(dto).subscribe({
      next: () => {
        this.toast.success('Device type added');

        this.newTypeName = '';
        this.newTypeIcon = '📦';

        this.loadDeviceTypes();
      },
      error: () => this.toast.error('Failed to add type')
    });
  }

  deleteType(id: number) {
    if (!confirm('Delete this device type?')) return;

    this.typeService.delete(id).subscribe({
      next: () => {
        this.toast.success('Deleted');
        this.loadDeviceTypes();
      },
      error: () => this.toast.error('Delete failed')
    });
  }
}