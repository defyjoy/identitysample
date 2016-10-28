import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import '../config/rxjs.operators';

@Injectable()

export class ValueService {
    constructor(private http: Http) {

    }

    getValues(): Observable<string[]> {
        return this.http.get('api/values')
            .map((res: Response) => res.json());
    }
}