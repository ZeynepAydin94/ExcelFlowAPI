using ExcelFlow.Core.Dtos.UploadJob;
using ExcelFlow.Services.Interfaces;
using ExcelFlow.Web.Common.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExcelFlow.Upload.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : BaseController
    {
        private readonly IUploadJobService _uploadJobService;

        public ImportController(IUploadJobService uploadJobService)
        {
            _uploadJobService = uploadJobService;
        }

        /// <summary>
        /// Endpoint to create a new upload job.
        /// </summary>
        /// <param name="createUploadJobDto">Details of the upload job to be created</param>
        /// <returns>Created upload job details</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUploadJob(UploadJobInsertDto createUploadJobDto)
        {
            if (createUploadJobDto == null || string.IsNullOrWhiteSpace(createUploadJobDto.FileUrl))
            {
                return Error("File URL is required.");
            }

            var uploadJob = await _uploadJobService.CreateAsync(createUploadJobDto);
            return Success(uploadJob);
        }




    }
}