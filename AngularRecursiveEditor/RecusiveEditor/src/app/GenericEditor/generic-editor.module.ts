import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GenericNumberEditorComponent } from './generic-number-editor.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
    declarations: [ GenericNumberEditorComponent],
    imports: [ CommonModule, ReactiveFormsModule ],
    exports: [ GenericNumberEditorComponent],
    providers: [],
})
export class GenericEditorModule {

}
