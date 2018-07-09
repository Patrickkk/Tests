import { Component, OnInit } from '@angular/core';
import { Pin } from './pin';
import { PinsApiService } from './pinsapi.service';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';

@Component({
    selector: 'app-pin',
    templateUrl: './pin.component.html',
})
export class PinComponent implements OnInit {

    pin: Pin = null;

    constructor(private pinapi: PinsApiService, private route: ActivatedRoute) { }

    isGpio() {
        return this.pin.capabilities.indexOf(0) >= 0;
    }

    public toggle() {
        console.log('toggle');
        this.pinapi.toggle(this.pin.wiringPiPinNumber).subscribe(() => { });
    }

    ngOnInit() {
        this.route.params.subscribe(x => {
            this.pinapi.getPin(parseInt(x.id)).subscribe(pin => { this.pin = pin; console.log(this.pin); });
        });
    }
}
