using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Configurations
{
    internal class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.ToTable("topics");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TopicTitel).HasMaxLength(254).IsRequired();
            builder.Property(x => x.TopicText).HasMaxLength(1500).HasDefaultValue("");
            builder.Property(x => x.TopicPhoto).HasMaxLength(254).HasDefaultValue("");

            //builder.HasMany<UserProfile>(x => x.UserProfiles)
            //   .WithMany(x => x.Topics)
            //   .UsingEntity(x => x.ToTable("UsersTopics"));
            builder.HasMany(x => x.UserProfiles)
            .WithMany(x => x.Topics)
            .UsingEntity<UserProfileTopic>(
            l => l.HasOne<UserProfile>().WithMany().HasForeignKey(x => x.UserProfileId),
            l => l.HasOne<Topic>().WithMany().HasForeignKey(x => x.TopicId)
            );
        }
    }
}
