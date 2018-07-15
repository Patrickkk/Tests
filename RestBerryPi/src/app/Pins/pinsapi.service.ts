import { Injectable } from '@angular/core';
import { Pin, PhysicalPin } from './pin';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';

@Injectable()
export class PinsApiService {

    constructor(private httpClient: HttpClient) {
    }

    public toggle(wiringPiPinNumber: number) {
        const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        return this.httpClient.post('http://localhost:55322/api/gpio/toggle/' + wiringPiPinNumber, null);
    }

    public getPin(id: number) {
        // TODO: only get relevant pin.
        return this.httpClient.get<Pin>('http://localhost:55322/api/gpio/' + id);
    }

    public getPins() {
        return this.httpClient.get<Pin[]>('http://localhost:55322/api/gpio');
    }

    public getAllPins() {
        return this.httpClient.get<Pin[]>('http://localhost:55322/api/gpio');
    }
}

