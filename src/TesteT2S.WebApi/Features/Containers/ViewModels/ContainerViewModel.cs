namespace TesteT2S.WebApi.Features.Containers.ViewModels
{
    public class ContainerViewModel
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Cliente { get; set; }
        public int Tipo { get; set; }
        public ContainerEnumViewModel Status { get; set; }
        public ContainerEnumViewModel Categoria { get; set; }
    }
}
