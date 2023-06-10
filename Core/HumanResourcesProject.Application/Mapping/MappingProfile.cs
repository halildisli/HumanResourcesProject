using AutoMapper;
using HumanResourcesProject.Application.DTOs;
using HumanResourcesProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<EditPersonDto, Person>().ReverseMap();
            CreateMap<CreateEmployeeDto, Employee>().ReverseMap();
            CreateMap<UpdateEmployeeDto, Employee>().ReverseMap();
            CreateMap<Person, Employee>().ReverseMap();
            CreateMap<EditPersonDto, Employee>().ReverseMap();
            CreateMap<ListOfAdvancesDto, Advance>().ReverseMap();
            CreateMap<CreateManagerDto, Person>().ReverseMap();
            CreateMap<CreateCompanyDto, Company>().ReverseMap();
            CreateMap<ListOfExpenseDto, Expense>().ReverseMap();
        }
    }
}
