import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

import { DeviceTypesService } from '../../../../core/services/device-type.service';

@Component({
  selector: 'app-device-types',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './device-types.component.html',
  styleUrls: ['./device-types.component.css']
})
export class DeviceTypeComponent implements OnInit {

  form!: FormGroup;
  deviceTypes: any[] = [];

  isEditing = false;
  editId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private deviceTypeService: DeviceTypesService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadTypes();
  }

  // ✅ FORM (ONLY ADD ICON FIELD - NO OTHER CHANGE)
  initForm() {
    this.form = this.fb.group({
      name: ['', Validators.required],

      // ✅ ICON FIELD ADDED
      icon: ['📦', Validators.required]
    });
  }

  // ✅ LOAD DATA (UNCHANGED LOGIC)
  loadTypes() {
    this.deviceTypeService.getAll().subscribe(res => {
      this.deviceTypes = res;
    });
  }

  // ✅ CREATE / UPDATE (FIXED TO INCLUDE ICON)
  submit() {

    const dto = {
      id: this.editId!,
      name: this.form.value.name,
      icon: this.form.value.icon,
      isActive: true
    };

    if (this.isEditing && this.editId) {
      this.deviceTypeService.update(this.editId, dto).subscribe(() => {
        this.loadTypes();
        this.reset();
      });
    } else {
      this.deviceTypeService.create(dto).subscribe(() => {
        this.loadTypes();
        this.reset();
      });
    }
  }

  // ✅ EDIT (FIXED ICON PATCH ONLY)
  edit(item: any) {
    this.isEditing = true;
    this.editId = item.id;

    this.form.patchValue({
      name: item.name,
      icon: item.icon || '📦'
    });
  }

  // ✅ RESET (UNCHANGED LOGIC)
  reset() {
    this.form.reset({
      name: '',
      icon: '📦'
    });

    this.isEditing = false;
    this.editId = null;
  }

  // DELETE (UNCHANGED)
  delete(id: number) {
    this.deviceTypeService.delete(id).subscribe(() => {
      this.loadTypes();
    });
  }
}