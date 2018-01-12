import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
    selector: '[appEditorFieldHost]',
})
export class EditorFieldHostDirective {
    constructor(public viewContainerRef: ViewContainerRef) { }
}
