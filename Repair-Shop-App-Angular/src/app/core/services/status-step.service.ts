import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StatusStepDto, CreateStatusStepDto, UpdateStatusStepDto } from '../../shared/models/status-step.model';

@Injectable({
  providedIn: 'root'
})
export class StatusStepsService {
  private apiUrl = 'https://localhost:7242/api/StatusSteps';

  constructor(private http: HttpClient) { }

  getAll(): Observable<StatusStepDto[]> {
    return this.http.get<StatusStepDto[]>(this.apiUrl);
  }

  getById(id: number): Observable<StatusStepDto> {
    return this.http.get<StatusStepDto>(`${this.apiUrl}/${id}`);
  }

  create(step: CreateStatusStepDto): Observable<StatusStepDto> {
    return this.http.post<StatusStepDto>(this.apiUrl, step);
  }

  update(id: number, step: UpdateStatusStepDto): Observable<StatusStepDto> {
    return this.http.put<StatusStepDto>(`${this.apiUrl}/${id}`, step);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}