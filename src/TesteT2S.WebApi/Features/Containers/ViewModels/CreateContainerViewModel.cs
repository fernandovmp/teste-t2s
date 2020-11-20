using TesteT2S.WebApi.Features.Containers.Enums;

namespace TesteT2S.WebApi.Features.Containers.ViewModels
{
    public class CreateContainerViewModel
    {
        public string Numero { get; set; }
        public string Cliente { get; set; }
        public int Tipo { get; set; }
        public ContainerStatus Status { get; set; }
        public ContainerCategory Categoria { get; set; }
    }
}
