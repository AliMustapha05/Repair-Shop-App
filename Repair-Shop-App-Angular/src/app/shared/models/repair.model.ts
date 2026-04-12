export interface RepairDto {
  id: number;
  deviceId: number;
  currentStatusId: number;
  currentStatusName?: string;
  problemDescription: string;
  createdAt: string;
  completedAt: string | null;
  notes: string | null;
}

export interface CreateRepairDto {
  deviceId: number;
  problemDescription: string;
  currentStatusId: number;
}

export interface UpdateRepairDto {
  id: number;
  deviceId: number;
  problemDescription?: string;
  currentStatusId: number;
}