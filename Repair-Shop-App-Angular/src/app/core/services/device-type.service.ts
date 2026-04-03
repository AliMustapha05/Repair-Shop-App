import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DeviceTypeDto, CreateDeviceTypeDto, UpdateDeviceTypeDto } from '../models/device-type.model';

@Injectable({
  providedIn: 'root'
})
export class DeviceTypesService {
  private apiUrl = 'https://localhost:5001/api/DeviceTypes';

  constructor(private http: HttpClient) { }

  getAll(): Observable<DeviceTypeDto[]> {
    return this.http.get<DeviceTypeDto[]>(this.apiUrl);
  }

  getById(id: number): Observable<DeviceTypeDto> {
    return this.http.get<DeviceTypeDto>(`${this.apiUrl}/${id}`);
  }

  create(deviceType: CreateDeviceTypeDto): Observable<DeviceTypeDto> {
    return this.http.post<DeviceTypeDto>(this.apiUrl, deviceType);
  }

  update(id: number, deviceType: UpdateDeviceTypeDto): Observable<DeviceTypeDto> {
    return this.http.put<DeviceTypeDto>(`${this.apiUrl}/${id}`, deviceType);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}