
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClassListComponent } from './sf/class/list/class-list.component';
import { AppComponent } from './app.component';
import { AppPaths } from './app-paths';

const routes: Routes = [
  { path: '', redirectTo: AppPaths.home, pathMatch: 'full' },
  { path: AppPaths.home, component: AppComponent, pathMatch: 'full' },
  { path: AppPaths.classBase, redirectTo: AppPaths.classBase + AppPaths.list, pathMatch: 'full' },
  { path: AppPaths.classBase + AppPaths.list, component: ClassListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
