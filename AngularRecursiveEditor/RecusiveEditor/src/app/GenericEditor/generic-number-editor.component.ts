import { Component, OnInit } from '@angular/core';
import { GenericFieldEditorBase } from './generic-field-editor.base';
import { Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
    selector: 'app-generic-number-editor',
    templateUrl: './generic-number-editor.component.html',
})
export class GenericNumberEditorComponent extends GenericFieldEditorBase implements OnInit {
    @Input() fieldName: string;
    formControl: FormControl;

    constructor() {
        super();
    }

    ngOnInit() { }
}
