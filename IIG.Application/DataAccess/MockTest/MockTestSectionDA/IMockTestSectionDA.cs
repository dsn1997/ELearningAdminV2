using IIG.Application.Models.MockTestPart;
using IIG.Application.Models.MockTestSection;


namespace IIG.Application.Data;

public interface IMockTestSectionDA
{
    Task<IEnumerable<MockTestSectionDto>> GetListByMockTestAsync(Guid mockTestId, DateTime? publishedAt = null);
    Task<IEnumerable<MockTestSectionDto>> GetListByMockTestChallengeAsync(Guid mockTestId, DateTime? publishedAt = null);
    Task<IEnumerable<MockTestPartInfoDto>> GetListMocktespartsOfSWType(Guid mockTestSectionId);
}