import { Pipe, PipeTransform } from '@angular/core';
@Pipe({
  name: 'myCustomFilter'
})
export class MyCustomFilterPipe implements PipeTransform {
  transform(data: any, toFilter: string): any {
    if (!toFilter) { return data; }
    return data.filter((d: { value: string; }) => d.value === toFilter);
  }
}