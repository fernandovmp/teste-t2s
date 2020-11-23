import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { HandlingReport } from '../HandlingReport';
import { ReportService } from '../report.service';

interface HandlingTableData extends HandlingReport {
  expand: boolean;
}

interface HandlingReportData {
  dados: HandlingTableData[]
  totalExportacoes: number;
  totalImportacoes: number;
}

@Component({
  selector: 'app-handling-report',
  templateUrl: './handling-report.component.html',
  styleUrls: ['./handling-report.component.css']
})
export class HandlingReportComponent implements OnInit {

  reports$: Observable<HandlingReportData>

  constructor(private reportService: ReportService) { }

  ngOnInit(): void {
    this.reports$ = this.reportService.getHandlingReport()
      .pipe(switchMap(data => {
        return of({
          ...data,
          dados: data.dados.map(reportData => ({
            ...reportData,
            expand: false
          }))
        })
      }));
  }

}
