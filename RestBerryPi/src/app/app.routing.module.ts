import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home.component';
import { PinsComponent } from './Pins/pins.component';

const routes: Routes = [
    { path: 'pins', component: PinsComponent },
    { path: '', component: HomeComponent },
    // { path: '**', component: HomeComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
