import { Component, OnInit } from '@angular/core';
import { PinsApiService } from './pinsapi.service';
import { Pin } from './pin';
import { ChangeDetectionStrategy } from '@angular/core';
import { ErrorHandler } from '../cross-cutting/ErrorHandler';
import { Router } from '@angular/router';

@Component({
    selector: 'app-pins',
    templateUrl: './pins.component.html',
    styleUrls: ['./pins.scss']
    // changeDetection: ChangeDetectionStrategy.OnPush
})
export class PinsComponent implements OnInit {
    public pins: Pin[] = [];

    constructor(private pinsService: PinsApiService, private router: Router) {
    }

    navigateToPinPage(pin: Pin) {
        console.log(pin);
        this.router.navigate(['/pin', pin.pinNumber]);
    }

    ngOnInit() {
        this.pinsService.getPins().subscribe(pins => {
            console.log(pins);
            this.pins = pins;
            console.log(this.pins);
        }, ErrorHandler.HandleError);
    }
}

