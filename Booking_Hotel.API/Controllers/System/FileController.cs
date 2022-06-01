using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Booking_Hotel.Application.Service.SystemService;
using System.Threading.Tasks;
using System.Collections.Generic;
using Booking_Hotel.Application.Configuration;
using Booking_Hotel.Utilities;
using Booking_Hotel.Application.ViewModels.System;

namespace Booking_Hotel.Controllers.SystemController
{
    [AllowAnonymous]
    public class FileController : BaseApiController
    {
        private readonly IFileService _service;

        public FileController(IFileService service)
        {
            _service = service;
        }
     

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] FileDataViewModel model)
        {
            return StatusCodeResult(await _service.AddAsync(model));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] FileDataViewModel model)
        {
            return StatusCodeResult(await _service.UpdateAsync(model));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(int key)
        {
            return StatusCodeResult(await _service.DeleteAsync(key));
        }

        [HttpGet]
        public async Task<ActionResult> FindByIdAsync(int id)
        {
            return Ok(await _service.FindByIdAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult> PaginationAsync(ParamaterPagination paramater)
        {
            return Ok(await _service.PaginationAsync(paramater));
        }

        [HttpGet]
        public async Task<ActionResult> LoadDxoGridAsync(DataSourceLoadOptions loadOptions)
        {
            return Ok(await _service.LoadDxoGridAsync(loadOptions));
        }

        [HttpGet]
        public async Task<ActionResult> LoadDxoLookupAsync(DataSourceLoadOptions loadOptions)
        {
            return Ok(await _service.LoadDxoLookupAsync(loadOptions));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> UploadFileAsync([FromForm] IFormFile file, string pathFolder)
        {
            return StatusCodeResult(await _service.UploadFileAsync(file, pathFolder));
        }

        [HttpPost]
        public async Task<ActionResult> UploadMultipleFileAsync([FromForm] List<IFormFile> files, string pathFolder)
        {
            return StatusCodeResult(await _service.UploadMultipleFileAsync(files, pathFolder));
        }

        [HttpDelete]
        public async Task<ActionResult> OnRemoveFileAsync(string fileLocalName)
        {
            return StatusCodeResult(await _service.OnRemoveFileAsync(fileLocalName));
        }

        [HttpGet]
        public async Task<ActionResult> DownloadFileAsync(string fullPath)
        {
            var result = await _service.DownloadFileAsync(fullPath);
            if (result != null)
            {
                return File(result.Content, result.ContentType, result.FileOriginaName);
            }
            else return BadRequest("File không tìm thấy");
        }
    }
}
