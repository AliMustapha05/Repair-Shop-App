import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../services/api.service';
import { DeviceTypeDto, CreateDeviceTypeDto, UpdateDeviceTypeDto } from '../../shared/models/device-type.model';

@Injectable({
  providedIn: 'root'
})
export class DeviceTypesService {

  constructor(private api: ApiService) {}

  getAll(): Observable<DeviceTypeDto[]> {
    return this.api.get<DeviceTypeDto[]>('DeviceTypes');
  }

  getById(id: number): Observable<DeviceTypeDto> {
    return this.api.get<DeviceTypeDto>(`DeviceTypes/${id}`);
  }

  create(dto: CreateDeviceTypeDto): Observable<DeviceTypeDto> {
    return this.api.post<DeviceTypeDto>('DeviceTypes', dto);
  }

  update(id: number, dto: UpdateDeviceTypeDto): Observable<DeviceTypeDto> {
    return this.api.put<DeviceTypeDto>(`DeviceTypes/${id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.api.delete<void>(`DeviceTypes/${id}`);
  }
}