using AutoMapper;
using System.Reflection;

namespace IIG.Application.AutoMapper
{
    public class AutoMapProfile : Profile
    {
        public AutoMapProfile()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var typesWithAttribute = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetCustomAttributes(typeof(AutoMapAttribute), true).Any());

            foreach (var type in typesWithAttribute)
            {
                var attributes = type.GetCustomAttributes<AutoMapAttribute>();
                foreach (var attr in attributes)
                {
                    var map = CreateMap(attr.SourceType, type).ReverseMap();

                    // Apply các option từ attribute
                    attr.ApplyConfiguration(map);

                    // Xử lý IgnoreMapAttribute
                    foreach (var prop in type.GetProperties())
                    {
                        if (prop.GetCustomAttribute<IgnoreMapAttribute>() != null)
                        {
                            try
                            {
                                map.ForMember(prop.Name, opt => opt.Ignore());
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
        }
    }
}

