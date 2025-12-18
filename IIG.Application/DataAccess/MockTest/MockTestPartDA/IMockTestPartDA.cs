

using IIG.Application.Models.MockTestPart;

namespace IIG.Application.Data;

public interface IMockTestPartDA 
{
    Task<IEnumerable<MockTestPartInfoDto>> GetListByMockTestSectionIdAsync(Guid mockTestSectionId, DateTime? publishedAt = null);
    Task<MockTestPartDetailDto> GetDetailById(Guid id, DateTime? publishedAt = null);

}