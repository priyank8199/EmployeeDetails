using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeServer;
using POC.Model;

namespace EmployeeMiddleware
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<BaseEmployees, Employee>()
                .ForMember(dest =>
                dest.Id,
                opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest =>
                dest.Name,
                opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest =>
                dest.Company,
                opt => opt.MapFrom(src => src.UserCompany))
                .ForMember(dest =>
                dest.Post,
                opt => opt.MapFrom(src => src.UserDesignation));
        }
    }
}
