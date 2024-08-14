using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Booking.DAL.Configurations
{
    internal class UserProfileConfiguration: IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("user_profiles");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.UserName).HasMaxLength(100);
            builder.Property(x => x.UserSurname).HasMaxLength(100);
            builder.Property(x => x.UserPhone).HasMaxLength(25);
            builder.Property(x => x.IsUserPet).HasDefaultValue(false);
            builder.Property(x => x.CurrencyCodeId).HasDefaultValue("USD");
            builder.Property(x => x.UserId).IsRequired();

            builder.HasOne<User>(x => x.User)
              .WithOne(x => x.UserProfile)
              .HasForeignKey<UserProfile>(x => x.UserId);

            //builder.HasMany<Facility>(x => x.Facilities)
            //   .WithMany(x => x.UserProfiles)
            //   .UsingEntity("UsersFacilities");

            builder.HasMany(x => x.Facilities)
             .WithMany(x => x.UserProfiles)
             .UsingEntity<UserProfileFacility>(
             l => l.HasOne<Facility>().WithMany().HasForeignKey(x => x.FacilityId),
             l => l.HasOne<UserProfile>().WithMany().HasForeignKey(x => x.UserProfileId)
             );

            //builder.HasMany(x => x.Facilities)
            //.WithMany(x => x.UserProfiles)
            //.UsingEntity<UserProfileFacility>(
            //l => l.HasOne<Facility>().WithMany().HasForeignKey(x => x.FacilityId),
            //l => l.HasOne<UserProfile>().WithMany().HasForeignKey(x => x.UserProfileId)
            //);
        }
    }
}
