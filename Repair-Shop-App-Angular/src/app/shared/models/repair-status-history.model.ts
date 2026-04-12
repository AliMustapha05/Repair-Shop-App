export interface RepairStatusHistoryDto {
  id: number;
  repairId: number;
  statusStepId: number;
  note: string;
  changedAt: string;
}

export interface CreateRepairStatusHistoryDto {
  statusStepId: number;
  note: string;
}