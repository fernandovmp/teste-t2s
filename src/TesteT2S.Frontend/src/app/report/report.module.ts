import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportRoutingModule } from './report-routing.module';
import { HandlingReportComponent } from './handling-report/handling-report.component';
import { HttpClientModule } from '@angular/common/http';
import { NzTableModule } from 'ng-zorro-antd/table';


@NgModule({
  declarations: [HandlingReportComponent],
  imports: [
    CommonModule,
    NzTableModule,
    HttpClientModule,
    ReportRoutingModule
  ]
})
export class ReportModule { }
