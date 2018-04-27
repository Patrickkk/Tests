import { Component, OnInit } from '@angular/core';
import { Pin } from './pin';

@Component({
    selector: 'app-pin',
    templateUrl: './pin.component.html',
})
export class PinComponent implements OnInit {

    pin: Pin = {
        name: 'TestPin',
        capabilities: [1, 2]
    };

    constructor() { }

    ngOnInit() {

    }
}
