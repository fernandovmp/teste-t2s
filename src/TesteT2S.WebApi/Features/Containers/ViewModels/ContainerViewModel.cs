using TesteT2S.WebApi.ViewModels;

namespace TesteT2S.WebApi.Features.Containers.ViewModels
{
    public class ContainerViewModel
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Cliente { get; set; }
        public int Tipo { get; set; }
        public EnumViewModel Status { get; set; }
        public EnumViewModel Categoria { get; set; }
    }
}
