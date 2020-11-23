import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ContainerFormComponent } from './container-form/container-form.component';
import { ContainerListComponent } from './container-list/container-list.component';

const routes: Routes = [
  { path: 'containers', component: ContainerListComponent },
  { path: 'containers/novo', component: ContainerFormComponent, data: { isInEditMode: false } },
  { path: 'containers/editar/:id', component: ContainerFormComponent, data: { isInEditMode: true } }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ContainerRoutingModule { }
