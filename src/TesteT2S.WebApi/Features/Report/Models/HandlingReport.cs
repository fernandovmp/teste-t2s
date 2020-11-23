using System.Collections.Generic;
namespace TesteT2S.WebApi.Features.Report.Models
{
    public class HandlingReport
    {
        public string Customer { get; set; }
        public List<HandlingReportEntry> ReportEntries { get; set; } = new List<HandlingReportEntry>();
    }
}
