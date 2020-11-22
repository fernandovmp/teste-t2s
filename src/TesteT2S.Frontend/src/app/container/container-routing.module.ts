import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ContainerListComponent } from './container-list/container-list.component';

const routes: Routes = [
  { path: 'containers', component: ContainerListComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ContainerRoutingModule { }
