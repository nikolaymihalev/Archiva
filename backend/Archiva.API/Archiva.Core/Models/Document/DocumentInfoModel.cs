namespace Archiva.Core.Models.Document
{
    /// <summary>
    /// Model for document information
    /// </summary>
    public class DocumentInfoModel
    {
        /// <summary>
        /// Unique idenfifier of the document
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the document
        /// </summary>
        public string Name { get; set; } = string.Empty;

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
        public DateTime EndDate { get; set; }
    }
}
