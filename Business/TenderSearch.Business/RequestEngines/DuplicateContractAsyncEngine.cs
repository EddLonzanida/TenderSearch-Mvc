using Eml.ConfigParser;
using Eml.DataRepository;
using Eml.Mediator.Contracts;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenderSearch.Business.Common.Entities;
using TenderSearch.Business.Extensions;
using TenderSearch.Business.Requests;
using TenderSearch.Business.Responses;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Data.Contracts;

namespace TenderSearch.Business.RequestEngines
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DuplicateContractAsyncEngine : IRequestAsyncEngine<DuplicateContractAsyncRequest, DuplicateContractResponse>
    {
        private readonly IDataRepositorySoftDeleteInt<Contract> dataRepository;

        private readonly IConfigBase<string, MainDbConnectionString> tenderSearchConnectionString;

        [ImportingConstructor]
        public DuplicateContractAsyncEngine(IDataRepositorySoftDeleteInt<Contract> dataRepository, IConfigBase<string, MainDbConnectionString> tenderSearchConnectionString)
        {
            this.dataRepository = dataRepository;
            this.tenderSearchConnectionString = tenderSearchConnectionString;
        }

        public async Task<DuplicateContractResponse> GetAsync(DuplicateContractAsyncRequest request)
        {
            var response = await GetDuplicateModelsEf(request);
            //var response = await GetDuplicateModelsAdo(request, tenderSearchConnectionString.Value);
            return new DuplicateContractResponse(response);
        }

        private static async Task<List<Contract>> GetDuplicateModelsAdo(DuplicateContractAsyncRequest request, string connString)
        {
            var isEdit = request.ActionName == DuplicateNameAction.Edit ? 1 : -1;
            var cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetContractDuplicates"
            };
            cmd.Parameters.AddWithValue("@isEdit", isEdit);
            cmd.Parameters.AddWithValue("@companyId", request.CompanyId);
            cmd.Parameters.AddWithValue("@contractId", request.ContractId);
            cmd.Parameters.AddWithValue("@renewalDate", request.RenewalDate);
            cmd.Parameters.AddWithValue("@endDate", request.EndDate);

            List<Contract> models;
            using (var conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
                cmd.Connection = conn;
                var reader = await cmd.ExecuteReaderAsync();

                var dt = new DataTable();
                dt.Load(reader);
                models = dt.CreateListFromTable<Contract>();
            }

            return models;
        }

        private async Task<IEnumerable<Contract>> GetDuplicateModelsEf(DuplicateContractAsyncRequest request)
        {
            IEnumerable<Contract> items;
            if (request.ActionName == DuplicateNameAction.Create)
                items = await dataRepository.GetAsync(r => request.CompanyId == r.CompanyId);
            else
                items = await dataRepository.GetAsync(r => request.CompanyId == r.CompanyId && request.ContractId != r.Id);

            return items.Where(r => request.RenewalDate >= r.RenewalDate && request.RenewalDate <= r.EndDate
                                     || request.EndDate >= r.RenewalDate && request.EndDate <= r.EndDate);
        }

        public void Dispose()
        {
        }
    }
}
