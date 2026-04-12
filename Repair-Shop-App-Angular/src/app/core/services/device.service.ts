import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../services/api.service';
import { DeviceDto, CreateDeviceDto, UpdateDeviceDto } from '../../shared/models/device.model';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {

  constructor(private api: ApiService) {}

  getAll(): Observable<DeviceDto[]> {
    return this.api.get<DeviceDto[]>('Devices');
  }

  getById(id: number): Observable<DeviceDto> {
    return this.api.get<DeviceDto>(`Devices/${id}`);
  }

  create(dto: CreateDeviceDto): Observable<DeviceDto> {
    return this.api.post<DeviceDto>('Devices', dto);
  }

  update(id: number, dto: UpdateDeviceDto): Observable<DeviceDto> {
    return this.api.put<DeviceDto>(`Devices/${id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.api.delete<void>(`Devices/${id}`);
  }
}