export interface RepairDto {
  id: number;
  deviceId: number;
  issueDescription: string;
  statusStepId: number;
}

export interface CreateRepairDto {
  deviceId: number;
  issueDescription: string;
}

export interface UpdateRepairDto {
  id: number;
  deviceId: number;
  issueDescription: string;
  statusStepId: number;
}

export interface RepairStatusHistoryDto {
  statusStepId: number;
  date: string;
}