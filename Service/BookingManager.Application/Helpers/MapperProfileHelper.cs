using AutoMapper;
using AutoMapper.Internal;
using Microservice.Utility.Application.Interfaces;
using Microservice.Utility.Domain.SeedWork;
using BookingManager.Application.Queries.Models;
using BookingManager.Domain.Aggregates.BookingAggregate;

namespace BookingManager.Application.Helpers
{
    public class MapperProfileHelper : Profile
    {
        public MapperProfileHelper()
        {
            this.Internal().ForAllMaps((map, expr) => expr.AfterMap<MapCustomFieldAction>());

            CreateMap<Booking, BookingModel>()
                .ReverseMap();
        }
    }

    /// <summary>
    /// For auto mapping of custom fields (if any)
    /// </summary>
    public class MapCustomFieldAction : IMappingAction<object, object>
    {
        public void Process(object source, object destination, ResolutionContext context)
        {
            if (source is EntityPlus entity && destination is ICustomField cf)
            {
                cf.CustomFields = entity.CustomFields;
            }
        }
    }
}
