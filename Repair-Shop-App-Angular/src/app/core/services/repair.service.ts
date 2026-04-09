import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RepairDto, CreateRepairDto, UpdateRepairDto, RepairStatusHistoryDto } from '../../shared/models/repair.model';

@Injectable({
  providedIn: 'root'
})
export class RepairsService {
  private apiUrl = 'https://localhost:7242/api/Repairs';

  constructor(private http: HttpClient) { }

  getAll(): Observable<RepairDto[]> {
    return this.http.get<RepairDto[]>(this.apiUrl);
  }

  getById(id: number): Observable<RepairDto> {
    return this.http.get<RepairDto>(`${this.apiUrl}/${id}`);
  }

  create(repair: CreateRepairDto): Observable<RepairDto> {
    return this.http.post<RepairDto>(this.apiUrl, repair);
  }

  update(id: number, repair: UpdateRepairDto): Observable<RepairDto> {
    return this.http.put<RepairDto>(`${this.apiUrl}/${id}`, repair);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  addStatus(repairId: number, status: RepairStatusHistoryDto): Observable<RepairStatusHistoryDto> {
    return this.http.post<RepairStatusHistoryDto>(`${this.apiUrl}/${repairId}/status`, status);
  }

  getHistory(repairId: number): Observable<RepairStatusHistoryDto[]> {
    return this.http.get<RepairStatusHistoryDto[]>(`${this.apiUrl}/${repairId}/history`);
  }
}