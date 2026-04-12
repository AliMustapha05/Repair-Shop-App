import { DeviceTypeDto } from './device-type.model';

export interface DeviceDto {
  id: number;
  deviceTypeId: number;
  brand: string;
  model: string;
  serialNumber: string | null;
  ownerName: string;
  ownerPhone: string;

   deviceType?: DeviceTypeDto;
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