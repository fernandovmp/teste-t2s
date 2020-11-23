import { IApiEnum } from '../types/api';

export interface Handling {
  id: number;
  navio: string;
  tipoMovimentacao: IApiEnum;
  dataInicio: Date;
  dataFim: Date;
}
