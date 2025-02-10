using Archiva.Core.Constants;
using Archiva.Infrastructure.Constants;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Archiva.Core.Models
{
    /// <summary>
    /// Model for adding or editing Document
    /// </summary>
    public class DocumentFormModel
    {
        /// <summary>
        /// Unique idenfifier of the document
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the document
        /// </summary>
        [Required(ErrorMessage = ReturnMessages.Required)]
        [StringLength(ValidationEntityConstants.DocumentNameMaxLength,
            MinimumLength = ValidationEntityConstants.DocumentNameMinLength,
            ErrorMessage = ReturnMessages.StringLength)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Uploaded file from user for document image
        /// </summary>
        public IFormFile? ImageFile { get; set; }

        /// <summary>
        /// Image of the document
        /// </summary>
        public byte[] Image { get; set; } = new byte[128];

        /// <summary>
        /// Description of the document
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Issue date of the document
        /// </summary>
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// End date of the document
        /// </summary>
        [Required(ErrorMessage = ReturnMessages.Required)]
        public DateTime EndDate { get; set; }
    }
}
