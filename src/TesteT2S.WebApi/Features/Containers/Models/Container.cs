using System.Collections.Generic;
using TesteT2S.WebApi.Features.Containers.Enums;
using TesteT2S.WebApi.Features.ShipHandling.Models;

namespace TesteT2S.WebApi.Features.Containers.Models
{
    public class Container
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Customer { get; set; }
        public byte Type { get; set; }
        public ContainerStatus Status { get; set; }
        public ContainerCategory Category { get; set; }
        public List<Handling> Handlings { get; set; }
    }
}
