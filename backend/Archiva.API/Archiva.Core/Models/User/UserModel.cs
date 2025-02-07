namespace Archiva.Core.Models
{
    /// <summary>
    /// Model for user information
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Unique idenfifier of the user
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// First name of the user
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Last name of the user
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Password of the user
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
