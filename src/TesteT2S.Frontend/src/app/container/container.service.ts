import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { IApiPaginatedResource } from '../types/api';
import { Container, PostContainerData } from './Container';

interface IContainerServiceGetOptions {
  pagina?: number;
  tamanho?: number;
}

@Injectable({
  providedIn: 'root'
})
export class ContainerService {

  constructor(private http: HttpClient) { }

  getAll(options?: IContainerServiceGetOptions) {
    const params = new HttpParams()
      .append('pagina', String(options?.pagina ?? 1))
      .append('tamanho', String(options?.tamanho ?? 10));
    return this.http.get<IApiPaginatedResource<Container>>(`${environment.apiUrl}/containers`, {
      params: params
    });
  }

  getById(containerId: number) {
    return this.http.get<Container>(`${environment.apiUrl}/containers/${containerId}`);
  }

  post(container: PostContainerData) {
    return this.http.post(`${environment.apiUrl}/containers`, container);
  }

  update(containerId: number, container: PostContainerData) {
    return this.http.put(`${environment.apiUrl}/containers/${containerId}`, container);
  }

  delete(containerId: number) {
    return this.http.delete(`${environment.apiUrl}/containers/${containerId}`)
  }
}
