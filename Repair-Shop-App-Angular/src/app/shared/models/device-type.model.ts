export interface DeviceTypeDto {
  id: number;
  name: string;
  icon?: string;
  isActive: boolean;
}

export interface CreateDeviceTypeDto {
  name: string;
  icon?: string;
}

export interface UpdateDeviceTypeDto {
  id: number;
  name: string;
  isActive: boolean;
}