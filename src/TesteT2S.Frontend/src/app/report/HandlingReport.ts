import { IApiEnum } from '../types/api';

export interface HandlingReport {
  cliente: string;
  movimentacoes: HandlingEntry[]
}

export interface HandlingEntry {
  tipoMovimentacao: IApiEnum;
  quantidadeMovimentacoes: number;
}

export interface HandlingReportCollection {
  dados: HandlingReport[]
  totalExportacoes: number;
  totalImportacoes: number;
}
