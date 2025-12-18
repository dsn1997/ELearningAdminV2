using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace IIG.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableRateLimiting(WebConstants.RateLimiterPolicy.FixedRateLimiting)]
    public class BaseController : ControllerBase
    {
    }
}
