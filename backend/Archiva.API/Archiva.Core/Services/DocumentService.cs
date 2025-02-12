using Archiva.Core.Constants;
using Archiva.Core.Contracts;
using Archiva.Core.Models;
using Archiva.Core.Models.Document;
using Archiva.Infrastructure.Common;
using Archiva.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Document = Archiva.Infrastructure.Models.Document;

namespace Archiva.Core.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository repository;

        public DocumentService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task AddAsync(DocumentFormModel model, string userId)
        {
            var user = await repository.GetByIdAsync<User>(userId);

            if (user is null)
                throw new ArgumentException(string.Format(ReturnMessages.DoesntExist, "User"));                       

            try
            {
                var document = new Document()
                {
                    Name = model.Name,
                    Image = model.Image,
                    Description = model.Description,
                    IssueDate = model.IssueDate,
                    EndDate = model.EndDate,
                };

                await repository.AddAsync(document);

                var lastDocument = await repository.AllReadonly<Document>().LastAsync();

                var userDocument = new UserDocument()
                {
                    DocumentId = lastDocument.Id + 1,
                    UserId = user.Id,
                };


                await repository.AddAsync(userDocument);
                await repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException(ReturnMessages.OperationFailed);
            }
        }

        public async Task DeleteAsync(string userId, int id)
        {
            var document = await repository.GetByIdAsync<Document>(id);

            if (document is null)
                throw new ArgumentException(string.Format(ReturnMessages.DoesntExist, "Document"));

            var user = await repository.GetByIdAsync<User>(userId);

            if (user is null)
                throw new ArgumentException(string.Format(ReturnMessages.DoesntExist, "User"));

            repository.Delete(new UserDocument() { DocumentId = id, UserId = userId });

            await repository.SaveChangesAsync();
        }

        public async Task EditAsync(DocumentFormModel model, string userId)
        {
            var document = await repository.GetByIdAsync<Document>(model.Id);

            if (document is null)
                throw new ArgumentException(string.Format(ReturnMessages.DoesntExist, "Document"));

            var user = await repository.GetByIdAsync<User>(userId);

            if (user is null)
                throw new ArgumentException(string.Format(ReturnMessages.DoesntExist, "User"));

            var userDocument = await repository.AllReadonly<UserDocument>()
                .Where(x => x.UserId == userId && x.DocumentId == model.Id)
                .FirstOrDefaultAsync();

            if(userDocument is null)
                throw new ArgumentException(string.Format(ReturnMessages.DoesntExist, "Document"));

            document.Name = model.Name;
            document.Description = model.Description;
            document.Image = model.Image;
            document.IssueDate = model.IssueDate;
            document.EndDate = model.EndDate;

            await repository.SaveChangesAsync();    
        }

        public async Task<DocumentInfoModel> GetByIdAsync(int id)
        {
            var document = await repository.GetByIdAsync<Document>(id);

            if(document is null)
                throw new ArgumentException(string.Format(ReturnMessages.DoesntExist, "Document"));

            return new DocumentInfoModel()
            {
                Id = document.Id,
                Name = document.Name,
                Image = document.Image,
                Description = document.Description,
                IssueDate = document.IssueDate,
                EndDate = document.EndDate,
            };
        }

        public async Task<IEnumerable<DocumentInfoModel>> GetUserDocumentsAsync(string userId)
        {
            var user = await repository.GetByIdAsync<User>(userId);

            if (user is null)
                throw new ArgumentException(string.Format(ReturnMessages.DoesntExist, "User"));

            var documents = new List<DocumentInfoModel>();

            var userDocumentsIds = await repository.AllReadonly<UserDocument>()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.DocumentId)
                .ToListAsync();

            foreach(var item in userDocumentsIds)
            {
                var documentEn = await repository.GetByIdAsync<Document>(item.DocumentId);

                if (documentEn is null)
                    continue;

                var document = new DocumentInfoModel()
                {
                    Id = documentEn.Id,
                    Name = documentEn.Name,
                    Image = documentEn.Image,
                    Description = documentEn.Description,
                    IssueDate = documentEn.IssueDate,
                    EndDate = documentEn.EndDate,
                };

                documents.Add(document);
            }

            return documents;
        }
    }
}
