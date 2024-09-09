import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {ClassListComponent} from './sf/class/list/class-list.component';
import {AppComponent} from './app.component';
import {AppPaths} from './app-paths';
import {ClassDetailComponent} from "./sf/class/detail/class-detail.component";
import {SchoolListComponent} from "./sf/school/list/school-list.component";
import {HomeComponent} from "./common/home/home.component";

const routes: Routes = [
    {path: '', redirectTo: AppPaths.home, pathMatch: 'full'},
    // {path: '**', redirectTo: AppPaths.home},
    {path: AppPaths.home, component: HomeComponent, pathMatch: 'full'},
    {path: AppPaths.classBase, redirectTo: AppPaths.classBase + AppPaths.list, pathMatch: 'full'},
    {path: AppPaths.classBase + AppPaths.list, component: ClassListComponent},
    {path: AppPaths.classBase + AppPaths.detail, component: ClassDetailComponent},
    // {path: AppPaths.schoolBase, redirectTo: AppPaths.schoolBase + AppPaths.list, pathMatch: 'full'},
    // {path: AppPaths.schoolBase + AppPaths.list, component: SchoolListComponent}

];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
