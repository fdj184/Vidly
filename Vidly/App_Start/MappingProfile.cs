using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // domain to DTO
            CreateMap<Customer, CustomerDto>();
            CreateMap<Movie, MovieDto>();

            // DTO to domain
            CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<MovieDto, Movie>()
                .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}