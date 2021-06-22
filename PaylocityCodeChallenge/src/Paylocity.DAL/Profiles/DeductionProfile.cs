using AutoMapper;
using Paylocity.DAL.Data.Model;
using Paylocity.DAL.DTOs;

namespace Paylocity.DAL.Profiles
{
    public class DeductionProfile : Profile
    {
        public DeductionProfile()
        {
            CreateMap<Employee, EmployeeReadDTO>();
            CreateMap<Dependent, DependentDTO>();
            CreateMap<EmployeeCreateDTO, Employee>();
            CreateMap<EmployeeUpdateDTOs, Employee>();
            CreateMap<DependentDTO, Dependent>();
        }
    }
}
