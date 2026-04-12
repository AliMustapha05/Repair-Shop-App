import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../services/api.service';
import { StatusStepDto, CreateStatusStepDto, UpdateStatusStepDto } from '../../shared/models/status-step.model';

@Injectable({
  providedIn: 'root'
})
export class StatusStepsService {

  constructor(private api: ApiService) {}

  getAll(): Observable<StatusStepDto[]> {
    return this.api.get<StatusStepDto[]>('StatusSteps');
  }

  getById(id: number): Observable<StatusStepDto> {
    return this.api.get<StatusStepDto>(`StatusSteps/${id}`);
  }

  create(dto: CreateStatusStepDto): Observable<StatusStepDto> {
    return this.api.post<StatusStepDto>('StatusSteps', dto);
  }

  update(id: number, dto: UpdateStatusStepDto): Observable<StatusStepDto> {
    return this.api.put<StatusStepDto>(`StatusSteps/${id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.api.delete<void>(`StatusSteps/${id}`);
  }
}