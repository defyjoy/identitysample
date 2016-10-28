import { Component } from '@angular/core';
import { ValueService } from '../services/value.service';


@Component({
    selector: 'app-values',
    //template: require('./values.template.html')
    templateUrl: 'scripts/app/components/values.template.html'
})

export class ValuesComponent {
    values: string[] = null;
    subValues: string[] = null;

    constructor(private valueService: ValueService) {
        this.values = ['value'
            , 'value'
            , 'value'
            , 'value'
            , 'value'
            , 'value'];
        let message = 'Typescript is awesome';
        this.valueService.getValues().subscribe(values => this.subValues = values,
            error => console.log(error),
            () => console.log(this.subValues));

    }
}