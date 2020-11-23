import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HandlingReportCollection } from './HandlingReport';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(private http: HttpClient) { }

  getHandlingReport() {
    return this.http.get<HandlingReportCollection>(`${environment.apiUrl}/relatorios/movimentacoes`);
  }
}
