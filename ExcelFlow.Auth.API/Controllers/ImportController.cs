using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExcelFlow.Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        [HttpPost("submit-url")]
        public async Task<IActionResult> SubmitUrl(CreateUploadJobDto dto)
        {
            var result = await _uploadJobService.CreateAsync(dto);
            return Ok(result);
        }
    }

}
