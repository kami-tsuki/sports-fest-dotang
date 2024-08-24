// sports-fest-dotangular.client/src/app/app-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClassListComponent } from './sf/class/list/class-list.component';
import { AppComponent } from './app.component';
import { ClassAddComponent } from "./sf/class/add/class-add.component";
import { AppPaths } from './app-paths';

const routes: Routes = [
  { path: '', redirectTo: AppPaths.home, pathMatch: 'full' },
  { path: AppPaths.home, component: AppComponent, pathMatch: 'full' },
  { path: AppPaths.classBase + AppPaths.list, component: ClassListComponent },
  { path: AppPaths.classBase + AppPaths.add, component: ClassAddComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
