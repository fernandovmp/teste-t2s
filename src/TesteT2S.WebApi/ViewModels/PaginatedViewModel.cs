using System;
using System.Collections.Generic;

namespace TesteT2S.WebApi.ViewModels
{
    public class PaginatedViewModel<T>
    {
        public PaginatedViewModel()
        {
        }

        public PaginatedViewModel(int currentPage, int pageSize, int totalCount, IEnumerable<T> result)
        {
            PaginaAtual = currentPage;
            PaginasTotais = (int)Math.Ceiling(totalCount / (double)pageSize); ;
            Tamanho = pageSize;
            QuantidadeTotal = totalCount;
            Dados = result;
        }

        public int PaginaAtual { get; set; }
        public int PaginasTotais { get; set; }
        public int Tamanho { get; set; }
        public int QuantidadeTotal { get; set; }
        public IEnumerable<T> Dados { get; set; }
    }
}
