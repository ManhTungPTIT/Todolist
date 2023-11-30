using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models.Dtos;
using Todo.Models.Models;

namespace Todo.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Users, UserDto>();
            CreateMap<Notes, NoteDto>()
                .ForMember(dest => dest.EmployeeName , opt => opt.MapFrom(src => src.Users.UserName));
        }
    }
}
