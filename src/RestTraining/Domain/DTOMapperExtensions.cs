﻿using System;
using System.Linq;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Services;
using RestTraining.Domain;

namespace RestTraining.Common.DTO
{
    public static class DTOMapperExtensions
    {
        public static HotelNumberTypeDTO ToDTO(this HotelNumberType hotelNumberType)
        {
            return (HotelNumberTypeDTO)hotelNumberType;
        }

        public static HotelNumberType ToEntity(this HotelNumberTypeDTO hotelNumberTypeDTO)
        {
            return (HotelNumberType)hotelNumberTypeDTO;
        }

        public static WindowViewTypeDTO ToDTO(this WindowViewType windowViewType)
        {
            return (WindowViewTypeDTO)windowViewType;
        }

        public static WindowViewType ToEntity(this WindowViewTypeDTO windowViewTypeDTO)
        {
            return (WindowViewType)windowViewTypeDTO;
        }

        public static IncludeItemTypeDTO ToDTO(this IncludeItemType includeItemType)
        {
            return (IncludeItemTypeDTO)includeItemType;
        }

        public static IncludeItemType ToEntity(this IncludeItemTypeDTO includeItemTypeDTO)
        {
            return (IncludeItemType)includeItemTypeDTO;
        }

        public static HotelsAttractionTypeDTO ToDTO(this HotelsAttractionType hotelsAttractionType)
        {
            return (HotelsAttractionTypeDTO)hotelsAttractionType;
        }

        public static HotelsAttractionType ToEntity(this HotelsAttractionTypeDTO hotelsAttractionTypeDTO)
        {
            return (HotelsAttractionType)hotelsAttractionTypeDTO;
        }

        public static HotelNumberDTO ToDTO(this HotelNumber hotelNumber)
        {
            return new HotelNumberDTO
            {
                Id = hotelNumber.Id,
                IncludeItems = hotelNumber.IncludeItems.Select(x => x.ToDTO()).ToList(),
                HotelId = hotelNumber.HotelId,
                HotelNumberType = hotelNumber.HotelNumberType.ToDTO(),
                WindowViews = hotelNumber.WindowViews.Select(x => x.Type.ToDTO()).ToList()
            };
        }
        public static HotelNumber ToEntity(this HotelNumberDTO hotelNumberDTO)
        {
            return new HotelNumber
            {
                Id = hotelNumberDTO.Id,
                IncludeItems = hotelNumberDTO.IncludeItems.Select(x => x.ToEntity()).ToList(),
                HotelId = hotelNumberDTO.HotelId,
                HotelNumberType = hotelNumberDTO.HotelNumberType.ToEntity(),
                WindowViews = hotelNumberDTO.WindowViews.Select(x => new WindowView { Type = x.ToEntity() }).ToList()
            };
        }

        public static Hotel ToEntity(this HotelDTO hotelDTO)
        {
            if (hotelDTO.Type == HotelDTO.TypeDescriminator.Free)
                return new FreeReservationsHotel
                {
                    Address = hotelDTO.Address,
                    HotelNumbers = hotelDTO.HotelNumbers.Select(x => x.ToEntity()).ToList(),
                    Id = hotelDTO.Id,
                    Title = hotelDTO.Title,
                    Image = ImageService.ToPngImageBytes(hotelDTO.Image), 
                    Description = hotelDTO.Description,
                    HotelsAttractions = hotelDTO.HotelsAttractions.Select(x => x.ToEntity()).ToList()
                };
            else if (hotelDTO.Type == HotelDTO.TypeDescriminator.Bounded)
            {
                return new BoundedReservationsHotel
                {
                    Address = hotelDTO.Address,
                    HotelNumbers = hotelDTO.HotelNumbers.Select(x => x.ToEntity()).ToList(),
                    Id = hotelDTO.Id,
                    Title = hotelDTO.Title,
                    Image = ImageService.ToPngImageBytes(hotelDTO.Image),
                    Description = hotelDTO.Description,
                    HotelsAttractions = hotelDTO.HotelsAttractions.Select(x => x.ToEntity()).ToList()
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
                HotelNumbers = hotel.HotelNumbers.Select(x => x.ToDTO()).ToList(),
                Id = hotel.Id,
                Title = hotel.Title,
                Type = hotel is FreeReservationsHotel ? HotelDTO.TypeDescriminator.Free : HotelDTO.TypeDescriminator.Bounded,
                Image = hotel.Image,
                Description = hotel.Description,
                HotelsAttractions = hotel.HotelsAttractions.Select(x => x.ToDTO()).ToList()
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
                    Image = ImageService.ToPngImageBytes(hotelDTO.Image),
                    Description = hotelDTO.Description,
                    HotelsAttractions = hotelDTO.HotelsAttractions.Select(x => x.ToEntity()).ToList()
                };
        }
        public static FreeReservationsHotelDTO ToDTO(this FreeReservationsHotel hotel)
        {
            return new FreeReservationsHotelDTO
            {
                Address = hotel.Address,
                HotelNumbers = hotel.HotelNumbers.Select(x => x.ToDTO()).ToList(),
                Id = hotel.Id,
                Title = hotel.Title,
                Image = hotel.Image,
                Description = hotel.Description,
                HotelsAttractions = hotel.HotelsAttractions.Select(x => x.ToDTO()).ToList()
            };
        }

        public static HotelDTO ToBaseDTO(this FreeReservationsHotel hotel)
        {
            return new HotelDTO
            {
                Address = hotel.Address,
                HotelNumbers = hotel.HotelNumbers.Select(x => x.ToDTO()).ToList(),
                Id = hotel.Id,
                Title = hotel.Title,
                Type = HotelDTO.TypeDescriminator.Free,
                Image = hotel.Image,
                Description = hotel.Description,
                HotelsAttractions = hotel.HotelsAttractions.Select(x => x.ToDTO()).ToList()
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
                Image = ImageService.ToPngImageBytes(hotelDTO.Image),
                Description = hotelDTO.Description,
                HotelsAttractions = hotelDTO.HotelsAttractions.Select(x => x.ToEntity()).ToList()
            };
        }
        public static BoundedReservationsHotelDTO ToDTO(this BoundedReservationsHotel hotel)
        {
            return new BoundedReservationsHotelDTO
            {
                Address = hotel.Address,
                HotelNumbers = hotel.HotelNumbers.Select(x => x.ToDTO()).ToList(),
                Id = hotel.Id,
                Title = hotel.Title,
                Image = hotel.Image,
                Description = hotel.Description,
                HotelsAttractions = hotel.HotelsAttractions.Select(x => x.ToDTO()).ToList()
            };
        }
        public static HotelDTO ToBaseDTO(this BoundedReservationsHotel hotel)
        {
            return new HotelDTO
            {
                Address = hotel.Address,
                HotelNumbers = hotel.HotelNumbers.Select(x => x.ToDTO()).ToList(),
                Id = hotel.Id,
                Title = hotel.Title,
                Type = HotelDTO.TypeDescriminator.Bounded,
                Image = hotel.Image,
                Description = hotel.Description,
                HotelsAttractions = hotel.HotelsAttractions.Select(x => x.ToDTO()).ToList()
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
                IncludeItemType = includedItemDTO.IncludeItemType.ToEntity()
            };
        }
        public static IncludedItemDTO ToDTO(this IncludedItem includedItem)
        {
            return new IncludedItemDTO
            {
                Count = includedItem.Count,
                IncludeItemType = includedItem.IncludeItemType.ToDTO()
            };
        }

        public static HotelsAttraction ToEntity(this HotelsAttractionDTO hotelsAttractionDTO)
        {
            return new HotelsAttraction
            {
                Count = hotelsAttractionDTO.Count,
                Id = 0,
                HotelsAttractionType = hotelsAttractionDTO.HotelsAttractionType.ToEntity()
            };
        }
        public static HotelsAttractionDTO ToDTO(this HotelsAttraction hotelsAttraction)
        {
            return new HotelsAttractionDTO
            {
                Count = hotelsAttraction.Count,
                HotelsAttractionType = hotelsAttraction.HotelsAttractionType.ToDTO()
            };
        }

    }
}