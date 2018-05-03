import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home.component';
import { PinsComponent } from './pins/pins.component';
import { PinComponent } from './pins/pin.component';

const routes: Routes = [
    { path: 'pins', component: PinsComponent },
    { path: 'pin/:id', component: PinComponent },
    { path: '', component: HomeComponent },
    // { path: '**', component: HomeComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
