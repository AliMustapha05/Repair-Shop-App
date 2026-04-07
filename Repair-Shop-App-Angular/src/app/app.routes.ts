import { Routes } from '@angular/router';

// Features
import { DeviceListComponent } from './features/devices/components/device-list/device-list.component';
import { DeviceFormComponent } from './features/devices/components/device-form/device-form.component';
import { RepairListComponent } from './features/repairs/components/repair-list/repair-list.component';
import { RepairFormComponent } from './features/repairs/components/repair-form/repair-form.component';
import { RepairDetailComponent } from './features/repairs/components/repair-detail/repair-detail.component';
import { StatusStepsComponent } from './features/settings/components/status-steps/status-steps.component';
import { DashboardComponent } from './features/dashboard/components/dashboard/dashboard.component';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },

  // Dashboard
  { path: 'dashboard', component: DashboardComponent },

  // Devices
  { path: 'devices', component: DeviceListComponent },
  { path: 'devices/new', component: DeviceFormComponent },
  { path: 'devices/edit/:id', component: DeviceFormComponent },

  // Repairs
  { path: 'repairs', component: RepairListComponent },
  { path: 'repairs/new', component: RepairFormComponent },
  { path: 'repairs/edit/:id', component: RepairFormComponent },
  { path: 'repairs/detail/:id', component: RepairDetailComponent },

  // Settings
  { path: 'settings/status-steps', component: StatusStepsComponent },

  // Fallback
  { path: '**', redirectTo: '/dashboard' }
];