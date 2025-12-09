using IIG.Application.Services;
using IIG.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_LearningAdminV2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommonController : ControllerBase
    {

        private readonly ICommonService _commonService;
        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<List<Tag>> Get()
        {
            return await _commonService.GetAllTagsAsync();
            
        }
    }
}
