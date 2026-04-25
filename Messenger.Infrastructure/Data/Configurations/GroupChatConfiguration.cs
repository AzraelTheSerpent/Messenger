using Messenger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastructure.Data.Configurations;

public class GroupChatConfiguration : IEntityTypeConfiguration<GroupChat>
{
    public void Configure(EntityTypeBuilder<GroupChat> builder)
    {
        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(g => g.ImageUrl)
            .HasMaxLength(255);

        builder.HasOne(g => g.Owner)
            .WithMany(u => u.OwnedChats as IEnumerable<GroupChat>)
            .HasForeignKey(g => g.OwnerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}