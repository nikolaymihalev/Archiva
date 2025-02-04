using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Archiva.Infrastructure.Models
{
    /// <summary>
    /// Represents the User entity
    /// </summary>
    [Comment("Represents the User")]
    public class User
    {
        /// <summary>
        /// Unique idenfifier of the user
        /// </summary>
        [Key]
        [Comment("User identifier")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// First name of the user
        /// </summary>
        [Required]
        [Comment("User first name")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Last name of the user
        /// </summary>
        [Required]
        [Comment("User last name")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Email of the user
        /// </summary>
        [Required]
        [Comment("User email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Password of the user
        /// </summary>
        [Required]
        [Comment("User password")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Collection of user documents
        /// </summary>
        public ICollection<UserDocument> UserDocuments { get; set; } = new List<UserDocument>();
    }
}
