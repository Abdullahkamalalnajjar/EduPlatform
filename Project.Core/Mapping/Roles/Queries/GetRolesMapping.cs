using Project.Data.Entities.Users;

namespace Project.Core.Mapping.Roles
{
    public partial class RoleProfile : Profile
    {
        public void GetRolesMapping()
        {
            CreateMap<ApplicationRole, RoleResponse>();
        }
    }
}
