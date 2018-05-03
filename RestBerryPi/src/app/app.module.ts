import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app.routing.module';
import { HomeComponent } from './home.component';
import { PinsComponent } from './pins/pins.component';
import { PinComponent } from './pins/pin.component';
import { PinsApiService } from './pins/pinsapi.service';
import { HttpClientModule } from '@angular/common/http';
import { MatToolbarModule, MatIconModule, MatButtonModule } from '@angular/material';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    PinsComponent,
    PinComponent
  ],
  imports: [
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserModule,
    RouterModule,
  ],
  providers: [PinsApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
