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
import { MatListModule } from '@angular/material/list';
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
import {NavBarComponent} from './nav-bar/nav-bar.component';
import {HomeCoverComponent} from './main-layout/home-cover/home-cover.component';
import {AboutSectionComponent} from './main-layout/about-section/about-section.component';
import {LoginModalComponent} from './login-modal/login-modal.component';
import {StudentPageComponent} from './student-page/student-page.component';
import {TeacherPageComponent} from './teacher-page/teacher-page.component';
import {ContactComponent} from './contact/contact.component';
import {ReactiveFormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import {MainLayoutComponent} from './main-layout/main-layout.component';
import {UserPageComponent} from './user-page/user-page.component';
import {MatTableModule} from '@angular/material/table';
import {TeamPageComponent} from './team-page/team-page.component';
import {ClassPageComponent} from './class-page/class-page.component';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatFormFieldModule} from '@angular/material/form-field';
import {TopClassComponent} from './main-layout/top-class/top-class.component';
import {SportsFestivalComponent} from "@app/sports-festival/sports-festival.component";
import {ManagerComponentComponent} from "@app/manager-component/manager-component.component";
import {DisciplinePageComponent} from "@app/discipline-page/discipline-page.component";

@NgModule({

    declarations: [
        AppComponent,
        NavBarComponent,
        HomeCoverComponent,
        AboutSectionComponent,
        LoginModalComponent,
        StudentPageComponent,
        TeacherPageComponent,
        SportsFestivalComponent,
        ContactComponent,
        MainLayoutComponent,
        JudgeComponentComponent,
        ManagerComponentComponent,
        UserPageComponent,
        TeamPageComponent,
        ClassPageComponent,
        DisciplinePageComponent,
        TopClassComponent,


    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        AppRoutingModule,
        RouterModule,
        MatSnackBarModule,
        CommonModule,
        FormsModule,
        MatButtonModule,
        MatIconModule,
        MatSelectModule,
        MatInputModule,
        MatListModule,
        MatDialogModule,
        MatToolbar,
        MatCard,
        MatCardContent,
        MatCardTitle,
        MatCardActions,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatPaginatorModule,
        BrowserModule,
        BrowserAnimationsModule,
        FormsModule,
        MatTableModule,
        MatPaginatorModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
    ],
    bootstrap: [
        AppComponent
    ],
    providers: [
        CacheService,
        ApiService,
        provideAnimationsAsync()
    ],
    exports: [
    ]
})
export class AppModule {
}
