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
import {ClassDeleteModalComponent} from './sf/class/delete-modal/class-delete-modal.component';
import {MatDialogModule} from '@angular/material/dialog';
import {ClassEditAddModalComponent} from './sf/class/edit-add-modal/class-edit-add-modal.component';
import {ErrorBannerComponent} from './common/banners/error-banner/error-banner.component';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {ClassDetailComponent} from "./sf/class/detail/class-detail.component";
import {StudentListComponent} from './sf/student/student-list/student-list.component';
import {DetailBannerComponent} from './sf/tutor/detail-banner/detail-banner.component';
import {SchoolListComponent} from './sf/school/list/school-list.component';
import {SchoolCardComponent} from './sf/school/school-card/school-card.component';
import {HomeComponent} from './common/home/home.component';
import {CounterComponent} from "./common/counters/bas/counter/counter.component";
import {SchoolCounterComponent} from "./common/counters/school-counter/school-counter.component";
import {ClassCounterComponent} from "./common/counters/class-counter/class-counter.component";
import {TutorCounterComponent} from "./common/counters/tutor-counter/tutor-counter.component";
import {StudentCounterComponent} from "./common/counters/student-counter/student-counter.component";
import {UserCounterComponent} from "./common/counters/user-counter/user-counter.component";
import {Placement} from "./enums/placement.enum";
import {MatToolbar} from "@angular/material/toolbar";
import {MatCard, MatCardActions, MatCardContent, MatCardTitle} from "@angular/material/card";
import { SelectUserComponent } from './sf/user/select-user/select-user.component';
import { CreateUserComponent } from './sf/user/create-user/create-user.component';


@NgModule({
    bootstrap: [
        AppComponent
    ],
    declarations: [
        AppComponent,
        ClassListComponent,
        ClassDeleteModalComponent,
        ClassEditAddModalComponent,
        ErrorBannerComponent,
        ClassDetailComponent,
        StudentListComponent,
        DetailBannerComponent,
        SchoolCardComponent,
        SchoolListComponent,
        HomeComponent,
        CounterComponent,
        SchoolCounterComponent,
        ClassCounterComponent,
        TutorCounterComponent,
        StudentCounterComponent,
        UserCounterComponent,
        SelectUserComponent,
        CreateUserComponent,
        
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        AppRoutingModule,
        MatSnackBarModule,
        CommonModule,
        FormsModule,
        MatButtonModule,
        MatIconModule,
        MatSelectModule,
        MatInputModule,

        MatDialogModule,
        MatToolbar,
        MatCard,
        MatCardContent,
        MatCardTitle,
        MatCardActions,
    ],
    providers: [
        CacheService,
        ApiService,
        
        provideAnimationsAsync()
    ],
    exports: [
        SchoolCardComponent,
        
    ]
})
export class AppModule {
}
