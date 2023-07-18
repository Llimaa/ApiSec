using ApiSec.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiSec.Infrastructure.Persistence.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasOne(sc => sc.User)
            .WithMany(s => s.UserRoles)
            .HasForeignKey(sc => sc.IdUser);
    }
}
