import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AppPaths} from './app-paths';
import {StudentPageComponent} from './student-page/student-page.component';
import {TeacherPageComponent} from './teacher-page/teacher-page.component';
import {ManagerComponentComponent} from './manger-component/manager-component.component';
import {MainLayoutComponent} from './main-layout/main-layout.component';
import {UserPageComponent} from './user-page/user-page.component';
import {TeamPageComponent} from './team-page/team-page.component';
import {ClassPageComponent} from './class-page/class-page.component';
import {DisciplinePageComponent} from "@app/dicipline-page/discipline-page.component";


const routes: Routes = [
    {path: '', redirectTo: AppPaths.mainLayout, pathMatch: 'full'},
    {path: AppPaths.classBase, redirectTo: AppPaths.classBase + AppPaths.list, pathMatch: 'full'},
    {path: 'studentPage', component: StudentPageComponent},
    {path: 'userPage', component: UserPageComponent},
    {path: 'teamPage', component: TeamPageComponent},
    {path: 'classPage', component: ClassPageComponent},
    {path: 'disciplinePage', component: DisciplinePageComponent},
    {path: AppPaths.teacherPage, component: TeacherPageComponent, pathMatch: 'full'},
    {path: AppPaths.managerComponent, component: ManagerComponentComponent, pathMatch: 'full'},
    {path: AppPaths.mainLayout, component: MainLayoutComponent, pathMatch: 'full'}
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
