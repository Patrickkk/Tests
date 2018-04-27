import { Component, OnInit } from '@angular/core';
import { PinsService } from './pinsapi.service';
import { Pin } from './pin';
import { ChangeDetectionStrategy } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ErrorHandler } from '../cross-cutting/ErrorHandler';

@Component({
    selector: 'app-pins',
    templateUrl: './pins.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class PinsComponent implements OnInit {
    public pins: Pin[] = [];

    constructor(private pinsService: PinsService) {

    }

    navigateToPinPage(pin: Pin) {

    }

    ngOnInit() {
        this.pinsService.getPins().subscribe(pins => { this.pins = pins; }, ErrorHandler.HandleError);
    }
}

