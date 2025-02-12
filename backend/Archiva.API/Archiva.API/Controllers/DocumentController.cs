using Archiva.Core.Constants;
using Archiva.Core.Contracts;
using Archiva.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Archiva.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService documentService;

        public DocumentController(IDocumentService _documentService)
        {
            documentService = _documentService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserDocumentsAsync(string userId)
        {
            try
            {
                var documents = await documentService.GetUserDocumentsAsync(userId);

                return Ok(documents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDocumentAsync([FromBody] DocumentFormModel model, string userId)
        {
            try
            {
                await documentService.AddAsync(model, userId);

                return Ok(new { Message = string.Format(ReturnMessages.SuccessfulOperation, "added", "document")});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditDocumentAsync([FromBody] DocumentFormModel model, string userId)
        {
            try
            {
                await documentService.EditAsync(model, userId);

                return Ok(new { Message = string.Format(ReturnMessages.SuccessfulOperation, "edited", "document") });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDocumentAsync(string userId, int id)
        {
            try
            {
                await documentService.DeleteAsync(userId, id);

                return Ok(new { Message = string.Format(ReturnMessages.SuccessfulOperation, "deleted", "document") });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDocumentAsync(int id)
        {
            try
            {
                var document = await documentService.GetByIdAsync(id);

                return Ok(document);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
