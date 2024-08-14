using Microsoft.EntityFrameworkCore;
using Booking.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.EntityFrameworkCore.Extensions;

namespace Booking.DAL.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.UserEmail).HasMaxLength(254);
            builder.Property(x => x.PasswordSalt).HasMaxLength(128);
            builder.Property(x => x.PasswordDk).HasMaxLength(128);
            builder.Property(x => x.UpdatedBy).HasDefaultValue(0);

            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<UserRole>(
                l => l.HasOne<Role>().WithMany().HasForeignKey(x => x.RoleId),
                l => l.HasOne<User>().WithMany().HasForeignKey(x => x.UserId)
                );
            builder.HasMany<Book>(x => x.Books)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId)
               .HasPrincipalKey(x => x.Id);
            builder.HasMany<Review>(x => x.Reviews)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId)
               .HasPrincipalKey(x => x.Id);


            //builder.HasData(new List<User>()
            //{
            //    new User()
            //    {
            //        Id = 1,
            //        UserEmail = "admin",
            //        PasswordDk = "password",
            //        PasswordSalt = "Salt",
            //        UpdatedBy = 1
            //    }

            //});

        }
    }
}
