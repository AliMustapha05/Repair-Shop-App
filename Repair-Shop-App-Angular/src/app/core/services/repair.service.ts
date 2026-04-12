import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../core/services/api.service';

import { RepairDto, CreateRepairDto, UpdateRepairDto } from '../../shared/models/repair.model';
import { RepairStatusHistoryDto, CreateRepairStatusHistoryDto } from '../../shared/models/repair-status-history.model';

@Injectable({
  providedIn: 'root'
})
export class RepairsService {

  constructor(private api: ApiService) {}

  getAll(): Observable<RepairDto[]> {
    return this.api.get<RepairDto[]>('Repairs');
  }

  getById(id: number): Observable<RepairDto> {
    return this.api.get<RepairDto>(`Repairs/${id}`);
  }

  create(dto: CreateRepairDto): Observable<RepairDto> {
    return this.api.post<RepairDto>('Repairs', dto);
  }

  update(id: number, dto: UpdateRepairDto): Observable<RepairDto> {
    const payload = {
      deviceId: dto.deviceId,
      problemDescription: dto.problemDescription ?? '',
      currentStatusId: dto.currentStatusId
    };

    return this.api.put<RepairDto>(`Repairs/${id}`, payload);
  }

  addStatus(repairId: number, dto: CreateRepairStatusHistoryDto): Observable<any> {
    return this.api.post<any>(`Repairs/${repairId}/status`, dto);
  }

  getHistory(repairId: number): Observable<RepairStatusHistoryDto[]> {
    return this.api.get<RepairStatusHistoryDto[]>(`RepairStatusHistory/${repairId}`);
  }

  getLatestActivity(): Observable<any[]> {
    return this.api.get<any[]>('RepairStatusHistory/latest');
  }
}