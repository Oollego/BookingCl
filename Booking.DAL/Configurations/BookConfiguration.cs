using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Configurations
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("books");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.BookComment).HasMaxLength(1500).HasDefaultValue("");
            builder.Property(x => x.IsPhoneCall).HasDefaultValue(false);
            builder.Property(x => x.IsEmail).HasDefaultValue(false);
            builder.Property(x => x.RoomBookPrice).HasPrecision(10, 2);
            builder.Property(x => x.RoomId).IsRequired();
        }
    }
}
