export interface DeviceDto {
  id: number;
  brand: string;
  model: string;
  serialNumber: string | null;
  ownerName: string;
  ownerPhone: string;
  deviceTypeId: number;
}

export interface CreateDeviceDto {
  deviceTypeId: number;
  brand: string;
  model: string;
  serialNumber: string | null;
  ownerName: string;
  ownerPhone: string;
}

export interface UpdateDeviceDto extends CreateDeviceDto {
  id: number;
}