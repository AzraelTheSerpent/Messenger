using Messenger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastructure.Data.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
         builder.ToTable("Accounts");
        
         builder.HasKey(a => a.Id);
 
         builder.Property(a => a.Login)
                .IsRequired()
                .HasMaxLength(60);
 
         builder.Property(a => a.HashPassword)
                .IsRequired()
                .HasMaxLength(60);
 
         builder.HasIndex(a => a.Login)
                .IsUnique();
         
         builder.HasOne(a => a.User)
                .WithOne(u => u.Account)
                .HasForeignKey<Account>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}