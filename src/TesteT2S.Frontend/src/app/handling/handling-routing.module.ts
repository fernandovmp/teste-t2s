import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HandlingFormComponent } from './handling-form/handling-form.component';
import { HandlingListComponent } from './handling-list/handling-list.component';

const routes: Routes = [
  { path: 'movimentacoes', component: HandlingListComponent },
  {
    path: 'movimentacoes/container/:containerNumber/novo',
    component: HandlingFormComponent,
    data: { isInEditMode: false }
  },
  {
    path: 'movimentacoes/container/:containerNumber/editar/:id',
    component: HandlingFormComponent,
    data: { isInEditMode: true }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HandlingRoutingModule { }
