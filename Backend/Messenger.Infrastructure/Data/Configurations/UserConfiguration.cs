using Messenger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
       
        builder.HasKey(u => u.Id);

        builder.Navigation(u => u.ChatMembers)
            .HasField("_chatMembers")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(u => u.OwnedChats)
            .HasField("_ownedChats")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(u => u.Messages)
            .HasField("_messages")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(u => u.SecondName)
            .HasMaxLength(60);

        builder.Property(u => u.AvatarUrl)
            .HasMaxLength(255);
    }
}