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
        return this.httpClient.post('http://192.168.1.150:5000/api/gpio/toggle/' + wiringPiPinNumber, null);
    }

    public getPin(id: number) {
        // TODO: only get relevant pin.
        return this.httpClient.get<Pin>('http://192.168.1.150:5000/api/gpio/' + id);
    }

    public getPins() {
        return this.httpClient.get<Pin[]>('http://192.168.1.150:5000/api/gpio');
    }

    public getAllPins() {
        return this.httpClient.get<Pin[]>('http://192.168.1.150:5000/api/gpio');
    }

    public addOtherPins(pins: Pin[]): PhysicalPin[] {
        const nonFuctionalPins: PhysicalPin[] = [
            { physicalPinNumber: 1, name: '3.3v Power', capabilities: [] },
            { physicalPinNumber: 2, name: '5v Power', capabilities: [] },
            { physicalPinNumber: 4, name: '5v Power', capabilities: [] },
            { physicalPinNumber: 6, name: 'Ground', capabilities: [] },
            { physicalPinNumber: 9, name: 'Ground', capabilities: [] },
            { physicalPinNumber: 1, name: '3.3v Power', capabilities: [] },
            { physicalPinNumber: 1, name: '3.3v Power', capabilities: [] },
            { physicalPinNumber: 1, name: '3.3v Power', capabilities: [] },
            { physicalPinNumber: 1, name: '3.3v Power', capabilities: [] },
            { physicalPinNumber: 1, name: '3.3v Power', capabilities: [] },
        ];

        return nonFuctionalPins;
    }

    public wireingPinToOrderedPinNumber(wireingpinNumber: number): number {
        switch (wireingpinNumber) {
            case 0: return 11;
            case 1: return 12;
            case 2: return 13;
            case 3: return 15;
            case 4: return 16;
            case 5: return 18;
            case 6: return 25;
            case 7: return 7;
            case 8: return 3;
            case 9: return 5;
            case 10: return 24;
            case 11: return 26;
            case 12: return 19;
            case 13: return 21;
            case 14: return 23;
            case 15: return 8;
            case 16: return 10;
            case 1: return 12;
            case 1: return 12;
            case 1: return 12;
            case 1: return 12;
            case 1: return 12;
            case 1: return 12;
            case 1: return 12;
            case 1: return 12;
            case 1: return 12;
            case 1: return 12;
            case 1: return 12;
            case 1: return 12;
            case 1: return 12;
            case 1: return 12;
            case 1: return 12;
            case 1: return 12;
            case 1: return 12;
            case 1: return 12;
            case 1: return 12;

        }
    }
}

