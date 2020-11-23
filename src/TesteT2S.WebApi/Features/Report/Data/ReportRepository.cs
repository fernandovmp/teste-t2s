using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TesteT2S.WebApi.Features.Report.Models;

namespace TesteT2S.WebApi.Features.Report.Data
{
    public class ReportRepository : IReportRepository
    {
        private readonly IDbConnection _connection;

        public ReportRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<HandlingReportCollection> GetHandlingReport()
        {
            const string Query = @"
            select [Containers].Customer, [Handlings].HandlingType, Count([Handlings].HandlingType) as HandlingAmount
                from Handlings
                INNER JOIN [Containers] on [Handlings].[ContainerId] = [Containers].[Id]
                GROUP BY [Containers].Customer, [Handlings].[HandlingType]
                ORDER BY [Containers].Customer, [Handlings].[HandlingType]";
            IEnumerable<HandlingReportEntry> entries = await _connection
                .QueryAsync<HandlingReportEntry>(Query);
            IEnumerable<HandlingReport> data = entries.GroupBy(report => report.Customer)
                .Select(reportGroup => new HandlingReport
                {
                    Customer = reportGroup.Key,
                    ReportEntries = reportGroup.AsList()
                });
            const string QueryImportationCount = @"
            select Count(*) from [Containers]
                where [Category] = 0";
            const string QueryExportationCount = @"
            select Count(*) from [Containers]
                where [Category] = 1";

            int totalImportation = await _connection.ExecuteScalarAsync<int>(QueryImportationCount);
            int totalExportation = await _connection.ExecuteScalarAsync<int>(QueryExportationCount);
            var reportCollection = new HandlingReportCollection
            {
                Data = data,
                TotalExportation = totalExportation,
                TotalImportation = totalImportation
            };
            return reportCollection;
        }
    }
}
