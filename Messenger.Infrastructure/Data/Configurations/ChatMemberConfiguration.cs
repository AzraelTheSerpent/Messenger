using Messenger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastructure.Data.Configurations;

public class ChatMemberConfiguration : IEntityTypeConfiguration<ChatMember>
{
    public void Configure(EntityTypeBuilder<ChatMember> builder)
    {
        builder.ToTable("ChatMembers");

        // Составной первичный ключ
        builder.HasKey(cm => new { cm.UserId, cm.ChatId });

        builder.HasOne(cm => cm.User)
            .WithMany(u => u.ChatMembers)
            .HasForeignKey(cm => cm.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cm => cm.Chat)
            .WithMany(c => c.Members)
            .HasForeignKey(cm => cm.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        // Связь с последним прочитанным сообщением
        builder.HasOne(cm => cm.LastReadMessage)
            .WithMany()
            .HasForeignKey(cm => cm.LastReadMessageId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}