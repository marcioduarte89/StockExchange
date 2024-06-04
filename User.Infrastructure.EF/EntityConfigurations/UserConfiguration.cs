using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.Infrastructure.EF.EntityConfigurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<Domain.User>
{
    public void Configure(EntityTypeBuilder<Domain.User> builder)
    {
        builder
            .Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.PhoneNumber)
            .HasMaxLength(30);
    }
}