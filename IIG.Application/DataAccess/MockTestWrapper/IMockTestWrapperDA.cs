using IIG.Web.Data.Models;

namespace IIG.Web.Data.Services.Interfaces;

public interface IMockTestWrapperDA
{
    Task<IEnumerable<Menu_MockTestWrapperDto>> GetListByGroup(Guid GroupId,string languageCode);
    //Task<IEnumerable<Menu_MockTestWrapperDto>> GetListByPaging(int pageSize, int PageNum, short Type);
    Task<MockTestWrapperDetailDto> GetDetailById(Guid wrapperId, string languageCode);
}
