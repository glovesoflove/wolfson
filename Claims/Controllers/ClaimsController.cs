using Microsoft.AspNetCore.Mvc;

using Claims.Service;

namespace Claims.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaimsController : ControllerBase
    {
        private readonly ILogger<ClaimsController> _logger;
        private readonly IClaimsService _claimsService;

        public ClaimsController(
          ILogger<ClaimsController> logger,
          IClaimsService claimsService
          )
        {
            _logger = logger;
            _claimsService = claimsService;
        }

        [HttpGet]
        public async Task<IEnumerable<Parser.Department>> GetAsync()
        {
            return await _claimsService.Extract();
        }

        [HttpGet("hierarchy")]
        public async Task<IEnumerable<Parser.Department>> GetHierarchyAsync()
        {
            return await _claimsService.ExtractHierarchy();
        }
    }
}
