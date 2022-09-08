using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BussinessLogic.Data.Configuration
{
    public class AnswersConfiguration : IEntityTypeConfiguration<AnswersEntities>
    {
        public void Configure(EntityTypeBuilder<AnswersEntities> builder)
        {
            builder.Property(a => a.Content)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(a => a.ImageUrl)
                .HasMaxLength(250);

            builder.Property(c => c.VideoUrl)
                .HasMaxLength(250);

            builder.HasOne(a => a.Comment)
                .WithMany()
                .HasForeignKey(a => a.CommentId);
        }
    }
}
