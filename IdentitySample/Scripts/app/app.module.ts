import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule, JsonpModule, RequestOptions } from '@angular/http';

import { ValueService } from './services/value.service';

import HttpOptions from './config/http-options';
import ResponseOptions from './config/response-options';

import { IdentityApp } from './app.component';
import { ValuesComponent } from './components/values.component';

@NgModule({
    imports: [BrowserModule
        , HttpModule],
    declarations: [IdentityApp
        , ValuesComponent],
    bootstrap: [IdentityApp],
    providers: [ValueService,
        { provide: ResponseOptions, useClass: ResponseOptions },
        { provide: RequestOptions, useClass: HttpOptions }]
})
export class AppModule { }