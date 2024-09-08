using Auth.Domain.Entities;
using Auth.Shared.Constes;

namespace Auth.Features.Permissions.Services
{
    public static class PermissionsDataSeeding
    {
        public static IEnumerable<Permission> InitialItems =>
        [
            new Permission
            {
                Id = Ulid.Parse(UserConstes.Create_Permission_Id),
                RoleId =Ulid.Parse(RoleConstes.Admin_Role_Id),
                ResourceId = Ulid.Parse(UserConstes.Create_Resource_Id)
            },
            new Permission
            {
                Id = Ulid.Parse(UserConstes.Update_Permission_Id),
                RoleId =Ulid.Parse(RoleConstes.Admin_Role_Id),
                ResourceId = Ulid.Parse(UserConstes.Update_Resource_Id)
            },
            new Permission
            {
                Id = Ulid.Parse(UserConstes.Delete_Permission_Id),
                RoleId =Ulid.Parse(RoleConstes.Admin_Role_Id),
                ResourceId = Ulid.Parse(UserConstes.Delete_Resource_Id)
            },
            new Permission
            {
                Id = Ulid.Parse(UserConstes.Get_Id_Permission_Id),
                RoleId =Ulid.Parse(RoleConstes.Admin_Role_Id),
                ResourceId = Ulid.Parse(UserConstes.Get_Id_Resource_Id)
            },
            new Permission
            {
                Id = Ulid.Parse(UserConstes.Get_List_Filter_Permission_Id),
                RoleId =Ulid.Parse(RoleConstes.Admin_Role_Id),
                ResourceId = Ulid.Parse(UserConstes.Get_List_Filter_Resource_Id)
            },
        ];
    }
}