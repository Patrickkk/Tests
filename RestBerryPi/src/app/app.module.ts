import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app.routing.module';
import { HomeComponent } from './home.component';
import { PinsComponent } from './Pins/pins.component';
import { PinsService } from './Pins/pinsapi.service';
import { HttpClientModule } from '@angular/common/http';
import { MatToolbarModule, MatIconModule, MatButtonModule } from '@angular/material';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    PinsComponent,
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
  providers: [PinsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
