using ChenTsaiWei_BackendTest_MidLevel.Models.Entities;
using ChenTsaiWei_BackendTest_MidLevel.Models.Requests;

namespace ChenTsaiWei_BackendTest_MidLevel.Repositories.Interfaces {
    public interface IMyofficeAcpdRepository {
        Task<IEnumerable<MyofficeAcpd>> GetAllAsync();
        Task<MyofficeAcpd?> GetByIdAsync(string sid);
        Task<string> CreateAsync(CreateMyofficeAcpdRequest request);
        Task<int> UpdateAsync(string sid, UpdateMyofficeAcpdRequest request);
        Task<int> DeleteAsync(string sid);
    }
}