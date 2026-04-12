import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'statusColor',
  standalone: true 
})
export class StatusColorPipe implements PipeTransform {

  transform(status: string | null | undefined): string {

    if (!status) return 'gray';

    switch (status.toLowerCase()) {
      case 'ready for pickup':
        return 'green';

      case 'in progress':
        return 'orange';

      case 'canceled':
        return 'red';

      case 'received':
        return 'blue';

      case 'diagnosed':
        return 'purple';

      default:
        return 'gray';
    }
  }
}