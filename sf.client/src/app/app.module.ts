import {HttpClientModule} from '@angular/common/http';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {ClassListComponent} from "./sf/class/list/class-list.component";
import {CacheService} from "./services/cache.service";
import {ApiService} from "./services/api.service";
import {FormsModule} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {provideAnimationsAsync} from '@angular/platform-browser/animations/async';
import {DeleteModalComponent} from './sf/class/delete-modal/delete-modal.component';
import {MatDialogModule} from '@angular/material/dialog';
import {EditAddModalComponent} from './sf/class/edit-add-modal/edit-add-modal.component';


@NgModule({
    bootstrap: [
        AppComponent
    ],
    declarations: [
        AppComponent,
        ClassListComponent,
        DeleteModalComponent,
        EditAddModalComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        AppRoutingModule,
        CommonModule,
        FormsModule,
        MatButtonModule,
        MatIconModule,
        MatSelectModule,
        MatInputModule,
        MatDialogModule,

    ],
    providers: [
        CacheService,
        ApiService,
        provideAnimationsAsync()
    ]
})
export class AppModule {
}
