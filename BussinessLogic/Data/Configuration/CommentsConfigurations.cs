using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BussinessLogic.Data.Configuration
{
    public class CommentsConfigurations : IEntityTypeConfiguration<CommentsEntities>
    {
        public void Configure(EntityTypeBuilder<CommentsEntities> builder)
        {
            builder.Property(c => c.Content)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(c => c.ImageUrl)
                .HasMaxLength(250);

            builder.Property(c => c.VideoUrl)
                .HasMaxLength(250);

            builder.HasOne(c => c.Publication)
                .WithMany()
                .HasForeignKey(c => c.PublicationId);
        }
    }
}
