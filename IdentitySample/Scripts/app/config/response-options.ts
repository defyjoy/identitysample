import {
    BaseResponseOptions
    , ResponseOptions
    , ResponseOptionsArgs
} from '@angular/http';

import SecurityService from '../services/security.service';


export default class HttpOptions extends BaseResponseOptions {
    constructor(private securityService: SecurityService) {
        super();

    }

    merge(options?: ResponseOptionsArgs): ResponseOptions {
        if (options.status == 401) {
            this.securityService.authorize();
            return;
        }
        var result = super.merge(options);
        result.merge = this.merge;
        return result;
    }
}

