import { Component, OnInit } from '@angular/core';
import { GenericFieldEditorBase } from './generic-field-editor.base';
import { Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
    selector: 'app-generic-number-editor',
    template: '<ng-template app-editor-field-host></ng-template>',
})
export class GenericNumberEditorComponent extends GenericFieldEditorBase implements OnInit {
    @Input() fieldName: string;
    formControl: FormControl;

    constructor() {
        super();
    }


    loadComponent() {
        this.currentAddIndex = (this.currentAddIndex + 1) % this.ads.length;
        let adItem = this.ads[this.currentAddIndex];
    
        let componentFactory = this.componentFactoryResolver.resolveComponentFactory(adItem.component);
    
        let viewContainerRef = this.adHost.viewContainerRef;
        viewContainerRef.clear();
    
        let componentRef = viewContainerRef.createComponent(componentFactory);
        (<AdComponent>componentRef.instance).data = adItem.data;
      }

    ngOnInit() { 
        
    }
}
