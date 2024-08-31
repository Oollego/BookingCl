using AutoMapper;
using Booking.Application.Resources;
using Booking.Domain.Dto.Facility;
using Booking.Domain.Dto.Hotel;
using Booking.Domain.Dto.HotelComfort;
using Booking.Domain.Dto.HotelInfoCell;
using Booking.Domain.Dto.NearStation;
using Booking.Domain.Dto.Review;
using Booking.Domain.Dto.Room;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Interfaces.UnitsOfWork;
using Booking.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Digests;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IBaseRepository<Hotel> _hotelRepository = null!;
        private readonly IBaseRepository<User> _userRepository = null!;
        private readonly IBaseRepository<Room> _roomRepository = null!;
        private readonly IMapper _mapper = null!;
        private readonly IHotelUnitOfWork _hotelUnitOfWork = null!;

        private readonly ILogger _logger = null!;

        public HotelService(IBaseRepository<Hotel> hotelRepository, IBaseRepository<User> userRepository,
            IBaseRepository<Room> roomRepository, IMapper mapper, IHotelUnitOfWork hotelUnitOfWork)
        {
            _hotelRepository = hotelRepository;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
            _hotelUnitOfWork = hotelUnitOfWork;
        }



        public async Task<BaseResult<InfoHotelDto>> GetHotelInfoAsync(long hotelId, string? email)
        {
            if (hotelId < 1)
            {
                return new BaseResult<InfoHotelDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var hotel = await _hotelRepository.GetAll()
                .Where(x => x.Id == hotelId)
                .Include(x => x.Reviews)
                .Include(x => x.Rooms)
                    .ThenInclude(x => x.RoomImages)
                .Include(x => x.HotelComfortIconTypes)
                .Include(x => x.HotelInfoCells).ThenInclude(x => x.InfoIcon)
                .Include(x => x.Facilities).ThenInclude(x => x.FacilityGroup)
                .Select(x => new InfoHotelDto
                {
                    Id = x.Id,
                    HotelName = x.HotelName,
                    HotelAddress = x.HotelAddress,
                    HotelPhone = x.HotelPhone,
                    Stars = x.Stars,
                    HotelImage = x.HotelImage,
                    Description = x.Description,
                    ReviewQty = x.Reviews.Count(),
                    CheapestRoom = x.Rooms.Min(x => x.RoomPrice),
                    HotelComfort = x.HotelComfortIconTypes.Select(x => new HotelInfoComfortDto
                    {
                        ComfortName = x.ComfortName,
                        ComfortIcon = x.ComfortIcon,
                    }).ToList(),
                    Rating =
                        Math.Round(x.Reviews.Average(r =>
                            (r.FacilityScore + r.StaffScore + r.CleanlinessScore + r.ComfortScore + r.LocationScore + r.ValueScore) / 6), 1),
                    Score = new ScoreDto {
                        FacilityScore = Math.Round(x.Reviews.Average(x => x.FacilityScore), 1),
                        StaffScore = Math.Round(x.Reviews.Average(x => x.StaffScore), 1),
                        CleanlinessScore = Math.Round(x.Reviews.Average(x => x.CleanlinessScore), 1),
                        ComfortScore = Math.Round(x.Reviews.Average(x => x.ComfortScore), 1),
                        LocationScore = Math.Round(x.Reviews.Average(x => x.LocationScore), 1),
                        ValueScore = Math.Round(x.Reviews.Average(x => x.ValueScore), 1)
                    },
                    Images = x.Rooms.SelectMany(r => r.RoomImages.Select(ri => ri.ImageName)).ToList(),
                    Facilities = x.Facilities
                        .GroupBy(f => new { f.FacilityGroup.FacilityGroupName, f.FacilityGroup.FacilityGroupIcon })
                        .Select(g => new FacilityInfoDto
                        {
                            GroupName = g.Key.FacilityGroupName,
                            GroupIcon = g.Key.FacilityGroupIcon ?? "",
                            Facilities = g.Select(f => f.FacilityName).ToList()
                        }).ToList(),
                    InfoCell = x.HotelInfoCells.Select(x => new HotelInfoCellDto
                    {
                        TextLine_1 = x.TextLine_1,
                        TextLine_2 = x.TextLine_2,
                        InfoIcon = x.InfoIcon!.IconFileName
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (hotel == null)
            {
                _logger.Warning(ErrorMessage.HotelNotFound);
                return new BaseResult<InfoHotelDto>()
                {
                    ErrorMessage = ErrorMessage.HotelNotFound,
                    ErrorCode = (int)ErrorCodes.HotelNotFound
                };
            }

            if (email != null)
            {
               
                 var currancy = await _userRepository.GetAll()
                    .Where(u => u.UserEmail == email)
                    .Include(x => x.UserProfile).ThenInclude(x => x.Currency)
                    .Select(x => new { x.UserProfile.Currency!.CurrencyChar, x.UserProfile.Currency.ExchangeRate })
                    .FirstOrDefaultAsync();
                if (currancy != null)
                {
                    hotel.CheapestRoom = Math.Round(hotel.CheapestRoom / (decimal)currancy.ExchangeRate);
                    hotel.CurrencyChar = currancy.CurrencyChar;
                }
            }

            return new BaseResult<InfoHotelDto>()
            {
                Data = hotel,
            };
        }

        public async Task<CollectionResult<TopHotelDto>> GetTopHotels(int qty, int avgReview)
        {
            if (avgReview < 1 || avgReview > 10 || qty < 1 )
            {
                return new CollectionResult<TopHotelDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var hotels = await _hotelRepository.GetAll()
                .Include(rev => rev.Reviews)
                .Include(city => city.City).ThenInclude(city => city.Country)
                .Include(place => place.NearStations.Where(np => np.NearPlaceNameId == 4)).ThenInclude(pln => pln.NearStationName)
                .Include(room => room.Rooms)
                .Select(h => new TopHotelDto
                {
                    HotelId = h.Id,
                    Rating =
                        Math.Round(h.Reviews.Average(g => 
                        (g.FacilityScore + g.StaffScore + g.CleanlinessScore + g.ComfortScore + g.LocationScore + g.ValueScore)/6 ), 1),
                    ReviewsQt = h.Reviews.Count(),
                    HotelName = h.HotelName,
                    HotelCity = h.City.CityName,
                    HotelCountry = h.City.Country.CountryName,
                    Stars = h.Stars,
                    MinPrice =
                      h.Rooms.Min(x => x.RoomPrice),
                    HotelImage = h.HotelImage,
                    DistanceToCityCenter = h.NearStations.FirstOrDefault()!.Distance,
                    DistanceMetric = h.NearStations.FirstOrDefault()!.DistanceMetric

                })
                .OrderByDescending(x => x.ReviewsQt)
                .Take(qty).Where(x => x.Rating > avgReview)
                .ToListAsync();

 
            if ( hotels.Count == 0 )
            {
                _logger.Warning(ErrorMessage.HotelNotFound, hotels.Count);
                return new CollectionResult<TopHotelDto>()
                {
                    ErrorMessage = ErrorMessage.HotelNotFound,
                    ErrorCode = (int)ErrorCodes.HotelNotFound
                };
            }

            if (hotels == null)
            {
                _logger.Warning(ErrorMessage.HotelNotFound);
                return new CollectionResult<TopHotelDto>()
                {
                    ErrorMessage = ErrorMessage.HotelNotFound,
                    ErrorCode = (int)ErrorCodes.HotelNotFound
                };
            }

            return new CollectionResult<TopHotelDto>()
            {
                Data = hotels,
                Count = hotels.Count
            };
        }

        public async Task<BaseResult<SearchHotelResponseDto>> SearchHotelAsync(SearchHotelDto dto)
        {
            if (dto.CheckIn.Date < DateTime.UtcNow.Date || dto.CheckIn > dto.CheckOut ||
                dto.Adults < 1 || dto.Children < 0 || dto.Rooms < 1)
            {
                return new BaseResult<SearchHotelResponseDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            //var hotels = await _roomRepository.GetAll()
            //    .Include(x => x.BedTypes)
            //    .Where(bd => bd.BedTypes.Sum(bt => bt.Adult) >= dto.Adults && bd.BedTypes.Sum(bt => bt.Children) >= dto.Childrens ||
            //        bd.BedTypes.Sum(bt => bt.Adult) >= (dto.Adults + dto.Childrens))
            //    .Include(x => x.Hotel)
            //        .ThenInclude(x => x.City)
            //        .ThenInclude(x => x.Country)
            //    .Where(x => x.Hotel.City.Country.CountryName == dto.Place || x.Hotel.City.CityName == dto.Place)
            //    .Include(x => x.Books)
            //    .Where(x => !x.Books.Any(b => dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn))
            //    .Select(x => x.Hotel)
            //    .Distinct()
            //    .ToListAsync();
       //     var queryHotels = _hotelRepository.GetAll()
       //.Include(h => h.Rooms)
       //    .ThenInclude(r => r.BedTypes)
       //        .Where(h => h.Rooms.Any(r =>
       //            r.BedTypes.Sum(bd => bd.Adult) >= dto.Adults &&
       //            r.BedTypes.Sum(bd => bd.Children) >= dto.Childrens ||
       //            r.BedTypes.Sum(bd => bd.Adult) >= (dto.Adults + dto.Childrens)))
       //.Include(h => h.Rooms)
       //    .ThenInclude(r => r.Books).Where(x => !x.Rooms.Any(r => r.Books.Any(b => dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn)))
       //.Include(h => h.City)
       //    .ThenInclude(r => r.Country)
       //            .Where(h => h.City.CityName == dto.Place || h.City.Country.CountryName == dto.Place)
       //.Include(h => h.Reviews)
       ////.Where(h => h.Reviews.Average(r =>
       ////        (r.FacilityScore + r.StaffScore + r.CleanlinessScore + r.ComfortScore + r.LocationScore + r.ValueScore) / 6) == dto.)
       //.Include(h => h.HotelComfortIconTypes)
       //.Include(h => h.NearStations)
       //    .ThenInclude(ns => ns.NearStationName)
       //.Select(x => new HotelDto
       //{
       //    Id = x.Id,
       //    HotelName = x.HotelName,
       //    HotelAddress = x.HotelAddress,
       //    HotelPhone = x.HotelPhone,
       //    HotelImage = x.HotelImage,
       //    Description = x.Description,
       //    FixedDays = x.FixedDays,
       //    AvrReviews = Math.Round(x.Reviews.Average(r =>
       //        (r.FacilityScore + r.StaffScore + r.CleanlinessScore + r.ComfortScore + r.LocationScore + r.ValueScore) / 6), 1),
       //    ReviewQty = x.Reviews.Count(),
       //    MinRoomPrice = x.Rooms.Min(x => x.RoomPrice),
       //    HotelComforts = x.HotelComfortIconTypes.Select(hct => new HotelInfoComfortDto
       //    {
       //        ComfortName = hct.ComfortName,
       //        ComfortIcon = hct.ComfortIcon
       //    }).ToList(),
       //    NearStations = x.NearStations.Select(s => new NearStationDto
       //    {
       //        Id = s.Id,
       //        StationName = s.NearStationName.Name,
       //        Distance = s.Distance,
       //        DistanceMetric = s.DistanceMetric,
       //        StationIcon = s.NearStationName.Icon!
       //    }).ToList(),
       //});


            var queryHotels = _hotelRepository.GetAll()
                .Include(h => h.Rooms)
                    .ThenInclude(r => r.BedTypes)
                .Include(h => h.Rooms)
                    .ThenInclude(r => r.Books)
                .Include(h => h.City)
                    .ThenInclude(c => c.Country)
                .Include(h => h.HotelData)
                .Include(h => h.HotelComfortIconTypes)
                .Include(h => h.NearStations)
                    .ThenInclude(ns => ns.NearStationName)
                .Where(h =>
                    (h.City.CityName == dto.Place || h.City.Country.CountryName == dto.Place) &&

                    h.Rooms.Any(r =>
                        (r.BedTypes.Sum(bd => bd.Adult) >= dto.Adults && r.BedTypes.Sum(bd => bd.Children) >= dto.Children) ||
                        r.BedTypes.Sum(bd => bd.Adult) >= (dto.Adults + dto.Children)) &&

                    (h.Rooms.Count(r =>
                        !r.Books.Any(b => dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn)) >= dto.Rooms) &&

                    dto.Stars.Contains( h.Stars) &&
                    dto.Facilities.Any(f => f.Contains( )


                    
                )

            //    int[] ids = new int[] { 1, 2, 3, 45, 99 };
            //using (DatabaseEntities db = new DatabaseEntities())
            //{
            //    return db.Licenses.Where(
            //        i => i.license == mylicense
            //           && ids.Contains(i.number)
            //        ).ToList();
            //}
                .Select(x => new HotelDto
                {
                    Id = x.Id,
                    HotelName = x.HotelName,
                    HotelAddress = x.HotelAddress,
                    HotelPhone = x.HotelPhone,
                    HotelImage = x.HotelImage,
                    Description = x.Description,
                    FixedDays = x.FixedDays,
                    Rating = x.HotelData.Rating,
                    ReviewQty = x.HotelData.ReviewCount,
                    MinRoomPrice = x.HotelData.HotelMinRoomPrice,
                    HotelComforts = x.HotelComfortIconTypes.Select(hct => new HotelInfoComfortDto
                    {
                        ComfortName = hct.ComfortName,
                        ComfortIcon = hct.ComfortIcon
                    }).ToList(),
                    NearStations = x.NearStations.Select(s => new NearStationDto
                    {
                        Id = s.Id,
                        StationName = s.NearStationName.Name,
                        Distance = s.Distance,
                        DistanceMetric = s.DistanceMetric,
                        StationIcon = s.NearStationName.Icon!
                    }).ToList(),
                });

            var hotels = await queryHotels.Skip(0).Take(2).ToListAsync();

            if (hotels.Count == 0)
            {
                _logger.Warning(ErrorMessage.HotelNotFound, hotels.Count);
                return new BaseResult<SearchHotelResponseDto>()
                {
                    ErrorMessage = ErrorMessage.HotelNotFound,
                    ErrorCode = (int)ErrorCodes.HotelNotFound
                };
            }

            if (hotels == null)
            {
                _logger.Warning(ErrorMessage.HotelNotFound);
                return new BaseResult<SearchHotelResponseDto>()
                {
                    ErrorMessage = ErrorMessage.HotelNotFound,
                    ErrorCode = (int)ErrorCodes.HotelNotFound
                };
            }

            SearchHotelResponseDto hotelResponse = new SearchHotelResponseDto
            {
                Matches = queryHotels.Count(),
                Hotels = hotels,
                Count = hotels.Count()
            };


            return new BaseResult<SearchHotelResponseDto>()
            {
                Data = hotelResponse
            };

            //int Matches = hotels.Count();
            //int Star1 = hotels.Count(x => x.Star == 1);

            //var hotels2 = await _roomRepository.GetAll()
            //    .Include(x => x.BedTypes)
            //    .Where(r => r.BedTypes.Sum(bd => bd.Adult) >= dto.Adults && r.BedTypes.Sum(bd => bd.Children) >= dto.Childrens ||
            //        r.BedTypes.Sum(bd => bd.Adult) >= (dto.Adults + dto.Childrens))
            //    .Include(x => x.Hotel)
            //        .ThenInclude(x => x.City)
            //        .ThenInclude(x => x.Country)
            //    .Include(x => x.Hotel)
            //        .ThenInclude(x => x.Reviews)
            //    .Include(x => x.Hotel)
            //        .ThenInclude(x => x.HotelComfortIconTypes)
            //    .Include(x => x.Hotel)
            //        .ThenInclude(x => x.NearStations)
            //        .ThenInclude(x => x.NearStationName)
            //    .Where(x => x.Hotel.City.Country.CountryName == dto.Place || x.Hotel.City.CityName == dto.Place)
            //    .Include(x => x.Books)
            //    .Where(x => !x.Books.Any(b => dto.CheckIn < b.CheckOut && dto.CheckOut > b.CheckIn))
            //    .Select(x => new HotelDto
            //    {
            //        Id = x.Hotel.Id,
            //        HotelName = x.Hotel.HotelName,
            //        HotelAddress = x.Hotel.HotelAddress,
            //        HotelPhone = x.Hotel.HotelPhone,
            //        HotelImage = x.Hotel.HotelImage,
            //        Description = x.Hotel.Description,
            //        FixedDays = x.Hotel.FixedDays,
            //        AvrReviews = Math.Round(x.Hotel.Reviews.Average(r =>
            //            (r.FacilityScore + r.StaffScore + r.CleanlinessScore + r.ComfortScore + r.LocationScore + r.ValueScore) / 6), 1),
            //        ReviewQty = x.Hotel.Reviews.Count(),
            //        MinRoomPrice = x.Hotel.Rooms.Min(x => x.RoomPrice),
            //        HotelComforts = x.Hotel.HotelComfortIconTypes.Select(hct => new HotelInfoComfortDto
            //        {
            //            ComfortName = hct.ComfortName,
            //            ComfortIcon = hct.ComfortIcon
            //        }).ToList(),
            //        NearStations = x.Hotel.NearStations.Select(s => new NearStationDto
            //        {
            //            Id = s.Id,
            //            StationName = s.NearStationName.Name,
            //            Distance = s.Distance,
            //            DistanceMetric = s.DistanceMetric,
            //            StationIcon = s.NearStationName.Icon!
            //        }).ToList(),
            //    })
            //    .ToListAsync();

            //return new CollectionResult<HotelDto>()
            //{
            //    Data = hotels.Skip(20).Take(10).ToList(),
            //    Count = hotels.Count()
            //};
        }

        public async Task<BaseResult<CreateHotelResponseDto>> CreateHotelAsync(CreateHotelDto dto)
        {
            if (dto.Stars < 0 || dto.FixedDays < 0 || dto.CityId < 0 || dto == null)
            {
                return new BaseResult<CreateHotelResponseDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var hotel = await _hotelRepository.GetAll().Where(h => h.HotelName == dto.HotelName).FirstOrDefaultAsync();

            if (hotel != null)
            {
                return new BaseResult<CreateHotelResponseDto>()
                {
                    ErrorMessage = ErrorMessage.HotelAlreadyExists,
                    ErrorCode = (int)ErrorCodes.HotelAlreadyExists
                };
            }

            hotel = new Hotel
            {
                HotelName = dto.HotelName,
                HotelAddress = dto.HotelAddress,
                HotelPhone = dto.HotelPhone,
                HotelImage = dto.HotelImage,
                CityGuide = dto.CityGuide,
                Description = dto.Description,
                Stars = dto.Stars,
                FixedDays = dto.FixedDays,
                IsPet = dto.IsPet,
                CityId = dto.CityId
            };
            using (var transaction = await _hotelUnitOfWork.BeginTransactionAsync()) 
            {
                try
                {
                    hotel = await _hotelUnitOfWork.Hotels.CreateAsync(hotel);
                    await _hotelUnitOfWork.SaveChangesAsync();

                    HotelData hotelData = new HotelData
                    {
                        HotelId = hotel.Id
                    };

                    await _hotelUnitOfWork.HotelsData.CreateAsync(hotelData);
                    await _hotelUnitOfWork.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch(Exception)
                {
                   await transaction.RollbackAsync();
                }
            } 
            

            return new BaseResult<CreateHotelResponseDto>()
            {
                Data = _mapper.Map<CreateHotelResponseDto>(hotel)
            };
        }
    }
}
