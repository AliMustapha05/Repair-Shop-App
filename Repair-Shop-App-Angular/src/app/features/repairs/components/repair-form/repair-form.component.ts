import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { RepairsService } from '../../../../core/services/repair.service';
import { DeviceService } from '../../../../core/services/device.service';
import { StatusStepsService } from '../../../../core/services/status-step.service';

import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';

import { DeviceDto } from '../../../../shared/models/device.model';
import { StatusStepDto } from '../../../../shared/models/status-step.model';
import { CreateRepairDto, UpdateRepairDto } from '../../../../shared/models/repair.model';

@Component({
  selector: 'app-repair-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './repair-form.component.html',
  styleUrls: ['./repair-form.component.css']
})
export class RepairFormComponent implements OnInit {

  form: any;

  devices: DeviceDto[] = [];
  statuses: StatusStepDto[] = [];

  isEdit = false;
  repairId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private repairsService: RepairsService,
    private deviceService: DeviceService,
    private statusService: StatusStepsService,
    private loader: LoaderService,
    private toast: ToastService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initForm();

    this.repairId = Number(this.route.snapshot.paramMap.get('id'));
    this.isEdit = !!this.repairId;

    this.loadDevices();
    this.loadStatuses();

    if (this.isEdit) {
      this.loadRepair();
    }
  }

  initForm() {
    this.form = this.fb.group({
      deviceId: [null, Validators.required],
      problemDescription: ['', Validators.required],
      currentStatusId: [null, Validators.required]
    });
  }

  // ✅ LOAD SINGLE REPAIR FOR EDIT
  loadRepair() {
    this.repairsService.getById(this.repairId!).subscribe({
      next: (res) => {
        this.form.patchValue({
          deviceId: res.deviceId,
          problemDescription: res.problemDescription,
          currentStatusId: res.currentStatusId
        });
      },
      error: () => {
        this.toast.error('Failed to load repair');
      }
    });
  }

  loadDevices() {
    this.deviceService.getAll().subscribe({
      next: (res) => this.devices = res
    });
  }

  loadStatuses() {
    this.statusService.getAll().subscribe({
      next: (res) => {
        this.statuses = res;

        // ✅ IMPORTANT: DO NOT auto-select anything
        // user must manually choose status
        if (this.isEdit) {
          return; // keep existing repair status
        }

        // optional: clear selection in create mode
        this.form.get('currentStatusId')?.setValue(null);
      },
      error: () => {
        this.toast.error('Failed to load statuses');
      }
    });
  }

  // ✅ FIXED SUBMIT (CREATE + EDIT)
  submit() {

    if (this.form.invalid) {
      this.toast.error('Please fill all fields');
      return;
    }

    this.loader.show();

    // CREATE
    if (!this.isEdit) {

      const dto: CreateRepairDto = this.form.value;

      this.repairsService.create(dto).subscribe({
        next: () => {
          this.toast.success('Repair created successfully');
          this.loader.hide();
          this.router.navigate(['/repairs']);
        },
        error: (err) => {
          console.error(err);

          if (err.status === 409) {
            this.toast.error('This device already has an active repair');
          } else {
            this.toast.error('Create failed');
          }

          this.loader.hide();
        }
      });

    }

    // EDIT
    else {
      
    const dto: UpdateRepairDto = {
      id: this.repairId!,
      deviceId: this.form.value.deviceId,
      problemDescription: this.form.value.problemDescription,
      currentStatusId: this.form.value.currentStatusId
    };

    this.repairsService.update(this.repairId!, dto).subscribe({
      next: () => {
        this.toast.success('Repair updated successfully');
        this.loader.hide();
        this.router.navigate(['/repairs']).then(() => {
          window.location.reload();
        });
      },
      error: (err) => {
        console.error(err);
        this.toast.error('Update failed');
        this.loader.hide();
      }
    });
  }
  }
}