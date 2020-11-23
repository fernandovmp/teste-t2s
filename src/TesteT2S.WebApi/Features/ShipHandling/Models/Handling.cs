using System;
using TesteT2S.WebApi.Features.Containers.Models;
using TesteT2S.WebApi.Features.ShipHandling.Enums;

namespace TesteT2S.WebApi.Features.ShipHandling.Models
{
    public class Handling
    {
        public int Id { get; set; }
        public int ContainerId { get; set; }
        public Container Container { get; set; }
        public string Ship { get; set; }
        public HandlingType HandlingType { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
