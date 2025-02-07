using Archiva.Core.Constants;
using Archiva.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;

namespace Archiva.Core.Models
{
    /// <summary>
    /// Model for user registration.
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// The first name of the user
        /// </summary>
        [Required(ErrorMessage = ReturnMessages.Required)]
        [StringLength(ValidationEntityConstants.UserNameMaxLength,
            MinimumLength = ValidationEntityConstants.UserNameMinLength,
            ErrorMessage = ReturnMessages.StringLength)]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// The last name of the user
        /// </summary>
        [Required(ErrorMessage = ReturnMessages.Required)]
        [StringLength(ValidationEntityConstants.UserNameMaxLength,
            MinimumLength = ValidationEntityConstants.UserNameMinLength,
            ErrorMessage = ReturnMessages.StringLength)]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// The user's email address used for account registration
        /// </summary>
        [Required(ErrorMessage = ReturnMessages.Required)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        /// <summary>
        /// The user's password used for account registration and future authentication
        /// </summary>
        [Required(ErrorMessage = ReturnMessages.Required)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        /// <summary>
        /// A field to confirm the user's password, ensuring the password is entered
        /// </summary>
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
