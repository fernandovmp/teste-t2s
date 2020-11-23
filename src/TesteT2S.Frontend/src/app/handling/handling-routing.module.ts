import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HandlingListComponent } from './handling-list/handling-list.component';

const routes: Routes = [
  { path: 'movimentacoes', component: HandlingListComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HandlingRoutingModule { }
