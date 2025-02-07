using Archiva.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Archiva.Core.Models
{
    /// <summary>
    /// Model for user login.
    /// Contains the user's email and password required for authentication during the login process.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// The user's email address used for login authentication
        /// </summary>
        [Required(ErrorMessage = ReturnMessages.Required)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        /// <summary>
        /// The user's password used for login authentication
        /// </summary>
        [Required(ErrorMessage = ReturnMessages.Required)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
