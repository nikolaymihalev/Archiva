using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Archiva.Infrastructure.Models
{
    /// <summary>
    /// Represents mapping between User and Document
    /// </summary>
    [Comment("Represents mapping between User and Document")]
    public class UserDocument
    {
        /// <summary>
        /// Unique idenfifier of the user
        /// </summary>
        [Required]
        [Comment("User identifier")]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Unique idenfifier of the document
        /// </summary>
        [Required]
        [Comment("Document identifier")]
        public int DocumentId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [ForeignKey(nameof(DocumentId))]
        public Document Document { get; set; } = null!;
    }
}
