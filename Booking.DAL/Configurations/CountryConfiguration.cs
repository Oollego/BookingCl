using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Booking.Domain.Entity;

namespace Booking.DAL.Configurations
{
    internal class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("countries");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CountryName).HasMaxLength(254).IsRequired();

            builder.HasMany<City>(x => x.Cities)
               .WithOne(x => x.Country)
               .HasForeignKey(x => x.CountryId)
               .HasPrincipalKey(x => x.Id);

            builder.HasData(new List<Country>()
            {
                new Country()
                {
                    Id = 1,
                    CountryName = "Albania",
                    FlagIcon = "albania.png"
                },
                new Country()
                {
                    Id = 2,
                    CountryName = "Andorra",
                    FlagIcon = "andorra.png"
                },
                new Country()
                {
                    Id = 3,
                    CountryName = "Armenia",
                    FlagIcon = "armenia.png"
                },
                new Country()
                {
                    Id = 4,
                    CountryName = "Austria",
                    FlagIcon = "austria.png"
                },
                new Country()
                {
                    Id = 5,
                    CountryName = "Azerbaijan",
                    FlagIcon = "azerbaijan.png"
                },
                new Country()
                {
                    Id = 6,
                    CountryName = "Belgium",
                    FlagIcon = "belgium.png"
                },
                new Country()
                {
                    Id = 7,
                    CountryName = "Bosnia and Herzegovina",
                    FlagIcon = "bosnia_and_herzegovina.png"
                },
                new Country()
                {
                    Id = 8,
                    CountryName = "Bulgaria",
                    FlagIcon = "bulgaria.png"
                },
                new Country()
                {
                    Id = 9,
                    CountryName = "Croatia",
                    FlagIcon = "croatia.png"
                },
                new Country()
                {
                    Id = 10,
                    CountryName = "Cyprus",
                    FlagIcon = "cyprus.png"
                },
                new Country()
                {
                    Id = 11,
                    CountryName = "Czechia",
                    FlagIcon = "czechia.png"
                },
                new Country()
                {
                    Id = 12,
                    CountryName = "Denmark",
                    FlagIcon = "denmark.png"
                },
                new Country()
                {
                    Id = 13,
                    CountryName = "Estonia",
                    FlagIcon = "estonia.png"
                },
                new Country()
                {
                    Id = 14,
                    CountryName = "Finland",
                    FlagIcon = "finland.png"
                },
                new Country()
                {
                    Id = 15,
                    CountryName = "France",
                    FlagIcon = "france.png"
                },
                new Country()
                {
                    Id = 16,
                    CountryName = "Georgia",
                    FlagIcon = "georgia.png"
                },
                new Country()
                {
                    Id = 17,
                    CountryName = "Germany",
                    FlagIcon = "germany.png"
                },
                new Country()
                {
                    Id = 18,
                    CountryName = "Greece",
                    FlagIcon = "greece.png"
                },
                new Country()
                {
                    Id = 19,
                    CountryName = "Hungary",
                    FlagIcon = "hungary.png"
                },
                new Country()
                {
                    Id = 20,
                    CountryName = "Iceland",
                    FlagIcon = "iceland.png"
                },
                new Country()
                {
                    Id = 21,
                    CountryName = "Ireland",
                    FlagIcon = "ireland.png"
                },
                new Country()
                {
                    Id = 22,
                    CountryName = "Italy",
                    FlagIcon = "italy.png"
                },
                new Country()
                {
                    Id = 23,
                    CountryName = "Latvia",
                    FlagIcon = "latvia.png"
                },
                new Country()
                {
                    Id = 24,
                    CountryName = "Liechtenstein",
                    FlagIcon = "liechtenstein.png"
                },
                new Country()
                {
                    Id = 25,
                    CountryName = "Lithuania",
                    FlagIcon = "lithuania.png"
                },
                new Country()
                {
                    Id = 26,
                    CountryName = "Luxembourg",
                    FlagIcon = "luxembourg.png"
                },
                new Country()
                {
                    Id = 27,
                    CountryName = "Malta",
                    FlagIcon = "malta.png"
                },
                
                new Country()
                {
                    Id = 28,
                    CountryName = "Monaco",
                    FlagIcon = "monaco.png"
                },
                new Country()
                {
                    Id = 29,
                    CountryName = "Montenegro",
                    FlagIcon = "montenegro.png"
                },
                new Country()
                {
                    Id = 30,
                    CountryName = "Netherlands",
                    FlagIcon = "netherlands.png"
                },
                new Country()
                {
                    Id = 31,
                    CountryName = "North Macedonia",
                    FlagIcon = "north_macedonia.png"
                },
                new Country()
                {
                    Id = 32,
                    CountryName = "Norway",
                    FlagIcon = "norway.png"
                },
                new Country()
                {
                    Id = 33,
                    CountryName = "Poland",
                    FlagIcon = "poland.png"
                },
                new Country()
                {
                    Id = 34,
                    CountryName = "Portugal",
                    FlagIcon = "portugal.png"
                },
                new Country()
                {
                    Id = 35,
                    CountryName = "Romania",
                    FlagIcon = "romania.png"
                },
                new Country()
                {
                    Id = 36,
                    CountryName = "San Marino",
                    FlagIcon = "san_marino.png"
                },
                new Country()
                {
                    Id = 37,
                    CountryName = "Slovak Republic",
                    FlagIcon = "slovak_republic.png"
                },
                new Country()
                {
                    Id = 38,
                    CountryName = "Slovenia",
                    FlagIcon = "slovenia.png"
                },
                new Country()
                {
                    Id = 39,
                    CountryName = "Spain",
                    FlagIcon = "spain.png"
                },
                new Country()
                {
                    Id = 40,
                    CountryName = "Sweden",
                    FlagIcon = "sweden.png"
                },
                new Country()
                {
                    Id = 41,
                    CountryName = "Switzerland",
                    FlagIcon = "switzerland.png"
                },
                new Country()
                {
                    Id = 42,
                    CountryName = "Türkiye",
                    FlagIcon = "turkiye.png"
                },
                new Country()
                {
                    Id = 43,
                    CountryName = "Ukraine",
                    FlagIcon = "ukraine.png"
                },
                new Country()
                {
                    Id = 44,
                    CountryName = "United Kingdom",
                    FlagIcon = "united_kingdom.png"
                },
                new Country()
                {
                    Id = 45,
                    CountryName = "Vatican",
                    FlagIcon = "vatican.png"
                },

                new Country()
                {
                    Id = 46,
                    CountryName = "Republic of Moldova",
                    FlagIcon = "moldova.png"
                },
            });
        }
    }
}
