using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BussinessLogic.Data.Configuration
{
    public class PublicationConfiguration : IEntityTypeConfiguration<PublicationsEntities>
    {
        public void Configure(EntityTypeBuilder<PublicationsEntities> builder)
        {
            builder.Property(p => p.Content)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(250);

            builder.Property(p => p.VideoUrl)
                .HasMaxLength(250);
            
        }
    }
}
