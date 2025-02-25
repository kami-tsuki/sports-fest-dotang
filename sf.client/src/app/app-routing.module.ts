import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AppPaths} from './app-paths';
import { StudentPageComponent } from './student-page/student-page.component';
import { TeacherPageComponent } from './teacher-page/teacher-page.component';
import { MangerComponentComponent } from './manger-component/manger-component.component';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { UserPageComponent } from './user-page/user-page.component';
import { TeamPageComponent } from './team-page/team-page.component';
import { ClassPageComponent } from './class-page/class-page.component';
import { DeciplienPageComponent } from './deciplien-page/deciplien-page.component';



const routes: Routes = [
    {path: '', redirectTo: AppPaths.mainLayout, pathMatch: 'full'},
    // {path: '**', redirectTo: AppPaths.home},
    //{path: AppPaths.home, component: HomeComponent, pathMatch: 'full'},
    {path: AppPaths.classBase, redirectTo: AppPaths.classBase + AppPaths.list, pathMatch: 'full'},
    //{path: AppPaths.classBase + AppPaths.list, component: ClassListComponent},
    //{path: AppPaths.classBase + AppPaths.detail, component: ClassDetailComponent},
    // {path: AppPaths.schoolBase, redirectTo: AppPaths.schoolBase + AppPaths.list, pathMatch: 'full'},
    // {path: AppPaths.schoolBase + AppPaths.list, component: SchoolListComponent}
    { path: 'studentPage', component: StudentPageComponent },
    { path: 'userPage', component: UserPageComponent },
    { path: 'teamPage', component: TeamPageComponent },
    { path: 'classPage', component: ClassPageComponent },
    { path: 'deciplienPage', component: DeciplienPageComponent },




    {path: AppPaths.teacherPage, component:TeacherPageComponent, pathMatch:'full'},
    {path: AppPaths.mangerComponent, component:MangerComponentComponent, pathMatch:'full'},
    {path: AppPaths.mainLayout,component: MainLayoutComponent, pathMatch:'full'}
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
