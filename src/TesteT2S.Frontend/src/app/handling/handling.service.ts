import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IApiGetOptions, IApiPaginatedResource } from '../types/api';
import { Handling } from './Handling';

export interface PostHandlingData {
  navio: string;
  tipoMovimentacao: number;
  dataInicio: Date;
  dataFim: Date;
}

@Injectable({
  providedIn: 'root'
})
export class HandlingService {

  constructor(private http: HttpClient) { }

  getAll(containerNumber: string, options?: IApiGetOptions) {
    let params = new HttpParams()
      .append('pagina', String(options?.pagina ?? 1))
      .append('tamanho', String(options?.tamanho ?? 10));
    if (options?.ordenarPor) {
      params = params.append('ordenar_por', options.ordenarPor);
    }
    return this.http.get<IApiPaginatedResource<Handling>>(
      `${environment.apiUrl}/containers/${containerNumber}/movimentacao`, {
      params: params
    });
  }

  getById(containerNumber: string, HandlingId: number) {
    return this.http.get<Handling>(
      `${environment.apiUrl}/containers/${containerNumber}/movimentacao/${HandlingId}`);
  }

  post(containerNumber: string, Handling: PostHandlingData) {
    return this.http.post(`${environment.apiUrl}/containers/${containerNumber}/movimentacao`, Handling);
  }

  update(containerNumber: string, HandlingId: number, Handling: PostHandlingData) {
    return this.http.put(
      `${environment.apiUrl}/containers/${containerNumber}/movimentacao/${HandlingId}`, Handling);
  }

  delete(containerNumber: string, HandlingId: number) {
    return this.http.delete(
      `${environment.apiUrl}/containers/${containerNumber}/movimentacao/${HandlingId}`)
  }
}
