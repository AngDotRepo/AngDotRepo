import { Pipe, PipeTransform } from "@angular/core"

@Pipe({
    name: "searchFilter"
})

export class SearchFilterPipe implements PipeTransform {
	transform(value: any, args?: any,colName: any="locationName") : any {
	 	if(!value) return null;
		if(!args) return value;

		args = args.toLowerCase();
		return value.filter(function(item: any) {
			return item[colName]
			    .toLowerCase()
			    .includes(args);

	});
}
}