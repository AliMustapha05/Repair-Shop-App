import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DeviceDto, CreateDeviceDto, UpdateDeviceDto } from '../../shared/models/device.model';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {
  private apiUrl = 'https://localhost:7242/api/Devices'; 

  constructor(private http: HttpClient) { }

  getAll(): Observable<DeviceDto[]> {
    return this.http.get<DeviceDto[]>(this.apiUrl);
  }

  getById(id: number): Observable<DeviceDto> {
    return this.http.get<DeviceDto>(`${this.apiUrl}/${id}`);
  }

  create(device: CreateDeviceDto): Observable<DeviceDto> {
    return this.http.post<DeviceDto>(this.apiUrl, device);
  }

  update(id: number, device: UpdateDeviceDto): Observable<DeviceDto> {
    return this.http.put<DeviceDto>(`${this.apiUrl}/${id}`, device);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}