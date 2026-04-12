import { Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout/layout.component';

export const routes: Routes = [

  // redirect
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },

  {
    path: '',
    component: LayoutComponent,
    children: [

      // ================= DASHBOARD =================
      {
        path: 'dashboard',
        loadComponent: () =>
          import('./features/dashboard/components/dashboard/dashboard.component')
            .then(m => m.DashboardComponent)
      },

      // ================= DEVICES =================
      {
        path: 'devices',
        loadComponent: () =>
          import('./features/devices/components/device-list/device-list.component')
            .then(m => m.DeviceListComponent)
      },
      {
        path: 'devices/new',
        loadComponent: () =>
          import('./features/devices/components/device-form/device-form.component')
            .then(m => m.DeviceFormComponent)
      },
      {
        path: 'devices/edit/:id',
        loadComponent: () =>
          import('./features/devices/components/device-form/device-form.component')
            .then(m => m.DeviceFormComponent)
      },

      // ================= REPAIRS =================
      {
        path: 'repairs',
        loadComponent: () =>
          import('./features/repairs/components/repair-list/repair-list.component')
            .then(m => m.RepairListComponent)
      },
      {
        path: 'repairs/new',
        loadComponent: () =>
          import('./features/repairs/components/repair-form/repair-form.component')
            .then(m => m.RepairFormComponent)
      },
      {
        path: 'repairs/edit/:id',
        loadComponent: () =>
          import('./features/repairs/components/repair-detail/repair-detail.component')
            .then(m => m.RepairDetailComponent)
      },

      // ================= SETTINGS (FIXED) =================
      {
        path: 'settings',
        loadComponent: () =>
          import('./features/settings/components/settings/settings.component')
            .then(m => m.SettingsComponent)
      }
    ]
  }
];