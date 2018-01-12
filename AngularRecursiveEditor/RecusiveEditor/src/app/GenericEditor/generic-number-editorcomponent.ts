import { Component, OnInit } from '@angular/core';
import { GenericFieldEditorBase } from './generic-field-editor.base';

@Component({
    selector: 'app-generic-number-editor',
    templateUrl: './generic-number-editor.component.html',
})
export class GenericNumberComponent extends GenericFieldEditorBase implements OnInit {
    constructor() {
        super();

    }

    ngOnInit() { }
}
