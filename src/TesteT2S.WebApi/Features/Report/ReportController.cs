using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TesteT2S.WebApi.Data;
using TesteT2S.WebApi.Features.Report.Data;
using TesteT2S.WebApi.Features.Report.Models;
using TesteT2S.WebApi.Features.Report.ViewModels;
using TesteT2S.WebApi.Features.ShipHandling.Models;

namespace TesteT2S.WebApi.Features.Report
{
    [ApiController]
    [Route("api/v1/relatorios")]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public ReportController(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        [HttpGet("movimentacoes")]
        public async Task<ActionResult<HandlingReportCollectionViewModel>> GetHandlingReport()
        {
            HandlingReportCollection report = await _reportRepository.GetHandlingReport();
            return _mapper.Map<HandlingReportCollectionViewModel>(report);
        }
    }
}
