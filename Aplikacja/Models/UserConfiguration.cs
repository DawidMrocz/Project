using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Aplikacja.Entities.UserModel;

namespace Aplikacja.Models
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(r => r.Name).IsRequired().HasMaxLength(10);
            builder.Property(r => r.Role).HasDefaultValue("User");
        }
    }
}
