import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HandlingReportComponent } from './handling-report/handling-report.component';

const routes: Routes = [
  { path: 'relatorios/movimentacao', component: HandlingReportComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRoutingModule { }
