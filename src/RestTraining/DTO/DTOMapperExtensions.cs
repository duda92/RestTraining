using System;
using System.Linq;
using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.DTO
{
    public static class DTOMapperExtensions
    {
        public static HotelNumberDTO ToDTO(this HotelNumber hotelNumber)
        {
            return new HotelNumberDTO
            {
                Id = hotelNumber.Id,
                IncludeItems = hotelNumber.IncludeItems.Select(x => x.ToDTO()).ToList(),
                HotelId = hotelNumber.HotelId,
                HotelNumberType = hotelNumber.HotelNumberType,
                WindowViews = hotelNumber.WindowViews.Select(x => x.Type).ToList()
            };
        }
        public static HotelNumber ToEntity(this HotelNumberDTO hotelNumberDTO)
        {
            return new HotelNumber
            {
                Id = hotelNumberDTO.Id,
                IncludeItems = hotelNumberDTO.IncludeItems.Select(x => x.ToEntity()).ToList(),
                HotelId = hotelNumberDTO.HotelId,
                HotelNumberType = hotelNumberDTO.HotelNumberType,
                WindowViews = hotelNumberDTO.WindowViews.Select(x => new WindowView { Type = x }).ToList()
            };
        }

        public static Hotel ToEntity(this HotelDTO hotelDTO)
        {
            if (hotelDTO.Type == HotelDTO.TypeDescriminator.Free)
                return new FreeReservationsHotel
                {
                    Address = hotelDTO.Address,
                    HotelNumbers = hotelDTO.HotelNumbers,
                    Id = hotelDTO.Id,
                    Title = hotelDTO.Title,
                };
            else if (hotelDTO.Type == HotelDTO.TypeDescriminator.Bounded)
            {
                return new BoundedReservationsHotel
                {
                    Address = hotelDTO.Address,
                    HotelNumbers = hotelDTO.HotelNumbers,
                    Id = hotelDTO.Id,
                    Title = hotelDTO.Title,
                };
            }
            else
                throw new InvalidCastException();
        }
        public static HotelDTO ToDTO(this Hotel hotel)
        {
            return new HotelDTO
            {
                Address = hotel.Address,
                HotelNumbers = hotel.HotelNumbers,
                Id = hotel.Id,
                Title = hotel.Title,
                Type = hotel is FreeReservationsHotel ? HotelDTO.TypeDescriminator.Free : HotelDTO.TypeDescriminator.Bounded
            };
        }

        public static FreeReservationsHotel ToEntity(this FreeReservationsHotelDTO hotelDTO)
        {
            return new FreeReservationsHotel
                {
                    Address = hotelDTO.Address,
                    HotelNumbers = hotelDTO.HotelNumbers.Select(x => x.ToEntity()).ToList(),
                    Id = hotelDTO.Id,
                    Title = hotelDTO.Title,
                };
        }
        public static FreeReservationsHotelDTO ToDTO(this FreeReservationsHotel hotel)
        {
            return new FreeReservationsHotelDTO
            {
                Address = hotel.Address,
                HotelNumbers = hotel.HotelNumbers.Select(x => x.ToDTO()).ToList(),
                Id = hotel.Id,
                Title = hotel.Title
            };
        }

        public static BoundedReservationsHotel ToEntity(this BoundedReservationsHotelDTO hotelDTO)
        {
            return new BoundedReservationsHotel
            {
                Address = hotelDTO.Address,
                HotelNumbers = hotelDTO.HotelNumbers.Select(x => x.ToEntity()).ToList(),
                Id = hotelDTO.Id,
                Title = hotelDTO.Title,
            };
        }
        public static BoundedReservationsHotelDTO ToDTO(this BoundedReservationsHotel hotel)
        {
            return new BoundedReservationsHotelDTO
            {
                Address = hotel.Address,
                HotelNumbers = hotel.HotelNumbers.Select(x => x.ToDTO()).ToList(),
                Id = hotel.Id,
                Title = hotel.Title
            };
        }

        public static ClientDTO ToDTO(this Client client)
        {
            return new ClientDTO
                {
                    //Id = client.Id,
                    Name = client.Name,
                    PhoneNumber = client.PhoneNumber
                };
        }
        public static Client ToEntity(this ClientDTO clientDTO)
        {
            return new Client
                {
                    Id = 0, //clientDTO.Id,
                    Name = clientDTO.Name,
                    PhoneNumber = clientDTO.PhoneNumber
                };
        }

        public static FreeBookingDTO ToDTO(this FreeBooking freeBooking)
        { 
            return new FreeBookingDTO
                {
                    BeginDate = freeBooking.BeginDate,
                    EndDate = freeBooking.EndDate,
                    Client = freeBooking.Client.ToDTO(),
                    HotelId = freeBooking.HotelId,
                    HotelNumberId = freeBooking.HotelNumberId,
                    Id = freeBooking.Id
                };
        }
        public static FreeBooking ToEntity(this FreeBookingDTO freeBookingDTO)
        {
            return new FreeBooking
            {
                BeginDate = freeBookingDTO.BeginDate,
                EndDate = freeBookingDTO.EndDate,
                Client = freeBookingDTO.Client.ToEntity(),
                //ClientId = freeBookingDTO.Client.Id,
                HotelId = freeBookingDTO.HotelId,
                HotelNumberId = freeBookingDTO.HotelNumberId,
                Id = freeBookingDTO.Id
            };
        }

        public static BoundedBookingDTO ToDTO(this BoundedBooking boundedBooking)
        {
            return new BoundedBookingDTO
            {
                Client = boundedBooking.Client.ToDTO(),
                HotelId = boundedBooking.HotelId,
                HotelNumberId = boundedBooking.HotelNumberId,
                Id = boundedBooking.Id,
                BoundedPeriodId = boundedBooking.BoundedPeriod.Id
            };

        }
        public static BoundedBooking ToEntity(this BoundedBookingDTO boundedBookingDTO)
        {
            var boundedBooking = new BoundedBooking
            {
                Client = boundedBookingDTO.Client.ToEntity(),
                HotelId = boundedBookingDTO.HotelId,
                HotelNumberId = boundedBookingDTO.HotelNumberId,
                Id = boundedBookingDTO.Id
            };
            boundedBooking.BoundedPeriod.Id = boundedBookingDTO.BoundedPeriodId;
            return boundedBooking;
        }

        public static BoundedPeriod ToEntity(this BoundedPeriodDTO boundedPeriodDTO)
        {
            return new BoundedPeriod
            {
                BeginDate = boundedPeriodDTO.BeginDate,
                BoundedReservationsHotelId = boundedPeriodDTO.BoundedReservationsHotelId,
                EndDate = boundedPeriodDTO.EndDate,
                Id = boundedPeriodDTO.Id
            };
        }
        public static BoundedPeriodDTO ToDTO(this BoundedPeriod boundedPeriod)
        {
            return new BoundedPeriodDTO
            {
                BeginDate = boundedPeriod.BeginDate,
                BoundedReservationsHotelId = boundedPeriod.BoundedReservationsHotelId,
                EndDate = boundedPeriod.EndDate,
                Id = boundedPeriod.Id
            };
        }

        public static IncludedItem ToEntity(this IncludedItemDTO includedItemDTO)
        {
            return new IncludedItem
            {
                Count = includedItemDTO.Count,
                Id = 0,
                IncludeItemType = includedItemDTO.IncludeItemType
            };
        }
        public static IncludedItemDTO ToDTO(this IncludedItem includedItem)
        {
            return new IncludedItemDTO
            {
                Count = includedItem.Count,
                IncludeItemType = includedItem.IncludeItemType
            };
        }

    }
}