using TesteT2S.WebApi.Features.ShipHandling.Enums;

namespace TesteT2S.WebApi.Features.Report.Models
{
    public class HandlingReportEntry
    {
        public string Customer { get; set; }
        public HandlingType HandlingType { get; set; }
        public int HandlingAmount { get; set; }
    }
}
