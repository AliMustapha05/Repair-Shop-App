import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'statusColor'
})
export class StatusColorPipe implements PipeTransform {
  transform(status: string): string {
    switch (status.toLowerCase()) {
      case 'ready for pickup':
        return 'green';
      case 'in progress':
        return 'orange';
      case 'canceled':
        return 'red';
      default:
        return 'gray';
    }
  }
}