using AutoMapper;
using IIG.Core.Common.Models.Files;

namespace IIG.Application.AutoMapper;
public abstract class BaseMappingProfile : Profile
{
    public BaseMappingProfile()
    {
        //CreateMap<IEnumerable<ImageInfoModel>, FileInsertModel>()
        //     .ConvertUsing(new ImageInfoConverter());
    }
}
