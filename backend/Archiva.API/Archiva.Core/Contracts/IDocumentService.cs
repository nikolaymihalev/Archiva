using Archiva.Core.Models;
using Archiva.Core.Models.Document;

namespace Archiva.Core.Contracts
{
    public interface IDocumentService
    {
        Task<IEnumerable<DocumentInfoModel>> GetUserDocumentsAsync(string userId);
        Task AddAsync(DocumentFormModel model);
        Task EditAsync(DocumentFormModel model);
        Task DeleteAsync(string userId, int id);
        Task<DocumentInfoModel> GetByIdAsync(int id);
    }
}
