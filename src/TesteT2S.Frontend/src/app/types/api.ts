export interface IApiEnum {
  id: number;
  descricao: string;
}

export interface IApiPaginatedResource<T> {
  pagina: number;
  paginaAtual: number;
  paginasTotais: number;
  tamanho: number;
  quantidadeTotal: number;
  dados: T[];
}

export interface IApiGetOptions {
  pagina?: number;
  tamanho?: number;
  ordenarPor?: string;
}
