using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Archiva.Infrastructure.Models
{
    /// <summary>
    /// Represents the Document entity
    /// </summary>
    [Comment("Represents the Document")]
    public class Document
    {
        /// <summary>
        /// Unique idenfifier of the document
        /// </summary>
        [Key]
        [Comment("Document identifier")]
        public int Id { get; set; }

        /// <summary>
        /// Name of the document
        /// </summary>
        [Required]
        [Comment("Document name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Image of the document
        /// </summary>
        [Comment("Document image")]
        public byte[] Image { get; set; } = new byte[128];

        /// <summary>
        /// Description of the document
        /// </summary>
        [Comment("Document description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Issue date of the document
        /// </summary>
        [Comment("Document issue date")]
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// End date of the document
        /// </summary>
        [Required]
        [Comment("Document end date")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Collection of user documents
        /// </summary>
        public ICollection<UserDocument> UserDocuments { get; set; } = new List<UserDocument>();
    }
}
