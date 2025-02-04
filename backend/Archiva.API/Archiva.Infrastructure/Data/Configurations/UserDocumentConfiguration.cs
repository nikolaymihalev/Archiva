using Archiva.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Archiva.Infrastructure.Data.Configurations
{
    internal class UserDocumentConfiguration : IEntityTypeConfiguration<UserDocument>
    {
        public void Configure(EntityTypeBuilder<UserDocument> builder)
        {
            builder.HasKey(x => new { x.UserId, x.DocumentId });

            builder.HasOne(x => x.User).WithMany(x => x.UserDocuments).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Document).WithMany(x => x.UserDocuments).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
