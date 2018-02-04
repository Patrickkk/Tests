import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { GenericEditorModule } from './GenericEditor/generic-editor.module';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    GenericEditorModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
