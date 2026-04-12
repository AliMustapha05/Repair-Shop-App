export interface StatusStepDto {
  id: number;
  name: string;
  description?: string;
  sortOrder?: number;
  isActive?: boolean;
}

export interface CreateStatusStepDto {
  name: string;
  description?: string;
  sortOrder?: number;
  isActive?: boolean;
}

export interface UpdateStatusStepDto {
  id: number;
  name: string;
  description?: string;
  sortOrder?: number;
  isActive?: boolean;
}