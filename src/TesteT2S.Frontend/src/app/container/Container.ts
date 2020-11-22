import { IApiEnum } from '../types/api';

export interface Container {
  id: number;
  numero: string;
  cliente: string;
  tipo: number;
  status: IApiEnum;
  categoria: IApiEnum;
}
