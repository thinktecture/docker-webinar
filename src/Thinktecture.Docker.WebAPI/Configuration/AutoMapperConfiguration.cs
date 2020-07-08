using System;
using AutoMapper;
using Thinktecture.Docker.WebAPI.Models;

namespace Thinktecture.Docker.WebAPI.Configuration
{
    public class CarProfile: Profile
    {
        public CarProfile()
        {
            CreateMap<CarDetailsModel, CarListModel>()
                .ForMember(c => c.Name, options => options.MapFrom(resolver => $"{resolver.Make} - {resolver.Model}"));
            CreateMap<CarCreateModel, CarDetailsModel>();
        }
    }
}
