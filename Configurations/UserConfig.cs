using Microsoft.EntityFrameworkCore;
using user_management_api_dotnet8.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace user_management_api_dotnet8.Configuration
{
    public class UserConfig:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasIndex(u=> u.Email)
                .IsUnique();

            builder.Property(u => u.HashPassword)
                .IsRequired();

            builder.Property(u => u.IsActive)
                .HasDefaultValue(true);
                
        }
    }
}
