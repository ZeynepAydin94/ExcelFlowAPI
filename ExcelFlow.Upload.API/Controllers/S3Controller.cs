using ExcelFlow.Services.Interfaces;
using ExcelFlow.Web.Common.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExcelFlow.Upload.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class S3Controller : BaseController
    {

        private readonly IAwsS3Service _awsS3Service;

        public S3Controller(IAwsS3Service awsS3Service)
        {
            _awsS3Service = awsS3Service;
        }

        [HttpPost("GeneratePreSignedUrl")]
        public IActionResult GeneratePreSignedUrl()
        {
            var response = _awsS3Service.GeneratePreSignedUploadUrl();
            return Ok(response);
        }
    }
}
