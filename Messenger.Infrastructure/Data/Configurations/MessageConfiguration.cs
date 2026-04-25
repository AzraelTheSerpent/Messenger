using Messenger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastructure.Data.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Messages");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Text)
            .HasMaxLength(3000);

        builder.Property(m => m.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp with time zone");

        builder.HasIndex(m => new { m.ChatId, m.CreatedAt })
            .HasDatabaseName("chat_history_ix");

        builder.HasOne(m => m.Sender)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.Chat)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}