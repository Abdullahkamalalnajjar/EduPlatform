using Project.Data.Entities.Users;
using Project.Core.Features.Authentication.Command.Models;

namespace Project.Core.Mapping.ApplicationUsers
{
    public partial class ApplicationUserProfile : Profile
    {
        public void CreateApplicationUserMapping()
        {
            CreateMap<SignUpUserCommand, ApplicationUser>()
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
              .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
              .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));
        }
    }
}
