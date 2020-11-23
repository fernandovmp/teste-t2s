using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteT2S.WebApi.Features.Report.Models;

namespace TesteT2S.WebApi.Features.Report.Data
{
    public interface IReportRepository
    {
        Task<HandlingReportCollection> GetHandlingReport();
    }
}
