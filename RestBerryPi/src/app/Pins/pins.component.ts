import { Component, OnInit } from '@angular/core';
import { PinsApiService } from './pinsapi.service';
import { Pin, GpioPinGroup } from './pin';
import { ChangeDetectionStrategy } from '@angular/core';
import { ErrorHandler } from '../cross-cutting/ErrorHandler';
import { Router } from '@angular/router';

@Component({
    selector: 'app-pins',
    templateUrl: './pins.component.html',
    styleUrls: ['./pins.scss']
})
export class PinsComponent implements OnInit {
    public pins: Pin[] = [];

    constructor(private pinsService: PinsApiService, private router: Router) {
    }

    navigateToPinPage(pin: Pin) {
        console.log(pin);
        this.router.navigate(['/pin', pin.pinNumber]);
    }

    pinTypeName(pin: Pin) {
        return GpioPinGroup[pin.pinType];
    }

    ngOnInit() {
        this.pinsService.getPins().subscribe(pins => {
            this.pins = pins;
        }, ErrorHandler.HandleError);
    }
}

