using AutoMapper;

namespace IIG.Application.Base
{
    public class ApplicationMarker { }
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<string, string>().ConvertUsing(s => string.IsNullOrEmpty(s) ? s : s.Trim());
          

        }
    }
}

