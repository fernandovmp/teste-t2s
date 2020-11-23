using System.Collections.Generic;

namespace TesteT2S.WebApi.Features.Report.Models
{
    public class HandlingReportCollection
    {
        public IEnumerable<HandlingReport> Data { get; set; }
        public int TotalExportation { get; set; }
        public int TotalImportation { get; set; }
    }
}
