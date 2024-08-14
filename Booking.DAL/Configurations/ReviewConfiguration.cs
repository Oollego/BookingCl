using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Configurations
{
    internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("reviews");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ReviewComment).HasMaxLength(1500).HasDefaultValue("");
            builder.Property(x => x.HotelId).IsRequired();
            builder.Property(x => x.FacilityScore).HasDefaultValue(0);
            builder.Property(x => x.StaffScore).HasDefaultValue(0);
            builder.Property(x => x.CleanlinessScore).HasDefaultValue(0);
            builder.Property(x => x.ComfortScore).HasDefaultValue(0);
            builder.Property(x => x.LocationScore).HasDefaultValue(0);
            builder.Property(x => x.ValueScore).HasDefaultValue(0);
            builder.ToTable(t => t.HasCheckConstraint("ValidFacilityScore", "FacilityScore >= 0 AND FacilityScore <= 10"));
            builder.ToTable(t => t.HasCheckConstraint("ValidStaffScore", "StaffScore >= 0 AND StaffScore <= 10"));
            builder.ToTable(t => t.HasCheckConstraint("ValidCleanlinessScore", "CleanlinessScore >= 0 AND CleanlinessScore <= 10"));
            builder.ToTable(t => t.HasCheckConstraint("ValidComfortScore", "ComfortScore >= 0 AND ComfortScore <= 10"));
            builder.ToTable(t => t.HasCheckConstraint("ValidLocationScore", "LocationScore >= 0 AND LocationScore <= 10"));
            builder.ToTable(t => t.HasCheckConstraint("ValidValueScore", "ValueScore >= 0 AND ValueScore <= 10"));
        }
    }
}
