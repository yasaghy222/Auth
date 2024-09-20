using Auth.Domain.Entities;
using Auth.Shared.Constes;

namespace Auth.Features.Permissions.Services
{
    public static class PermissionsDataSeeding
    {
        public static IEnumerable<Permission> InitialItems =>
        [
            #region  User Resources Permissions
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
            #endregion User Resources Permissions

            #region  Organization Resources Permissions
            new Permission
            {
                Id = Ulid.Parse(OrganizationConstes.Create_Permission_Id),
                RoleId =Ulid.Parse(RoleConstes.Admin_Role_Id),
                ResourceId = Ulid.Parse(OrganizationConstes.Create_Resource_Id)
            },
            new Permission
            {
                Id = Ulid.Parse(OrganizationConstes.Update_Permission_Id),
                RoleId =Ulid.Parse(RoleConstes.Admin_Role_Id),
                ResourceId = Ulid.Parse(OrganizationConstes.Update_Resource_Id)
            },
            new Permission
            {
                Id = Ulid.Parse(OrganizationConstes.Delete_Permission_Id),
                RoleId =Ulid.Parse(RoleConstes.Admin_Role_Id),
                ResourceId = Ulid.Parse(OrganizationConstes.Delete_Resource_Id)
            },
            new Permission
            {
                Id = Ulid.Parse(OrganizationConstes.Get_Id_Permission_Id),
                RoleId =Ulid.Parse(RoleConstes.Admin_Role_Id),
                ResourceId = Ulid.Parse(OrganizationConstes.Get_Id_Resource_Id)
            },
            new Permission
            {
                Id = Ulid.Parse(OrganizationConstes.Get_List_Filter_Permission_Id),
                RoleId =Ulid.Parse(RoleConstes.Admin_Role_Id),
                ResourceId = Ulid.Parse(OrganizationConstes.Get_List_Filter_Resource_Id)
            },
            #endregion Organization Resources Permissions

            #region  Role Resources Permissions
            new Permission
            {
                Id = Ulid.Parse(RoleConstes.Create_Permission_Id),
                RoleId =Ulid.Parse(RoleConstes.Admin_Role_Id),
                ResourceId = Ulid.Parse(RoleConstes.Create_Resource_Id)
            },
            new Permission
            {
                Id = Ulid.Parse(RoleConstes.Update_Permission_Id),
                RoleId =Ulid.Parse(RoleConstes.Admin_Role_Id),
                ResourceId = Ulid.Parse(RoleConstes.Update_Resource_Id)
            },
            new Permission
            {
                Id = Ulid.Parse(RoleConstes.Delete_Permission_Id),
                RoleId =Ulid.Parse(RoleConstes.Admin_Role_Id),
                ResourceId = Ulid.Parse(RoleConstes.Delete_Resource_Id)
            },
            new Permission
            {
                Id = Ulid.Parse(RoleConstes.Get_Id_Permission_Id),
                RoleId =Ulid.Parse(RoleConstes.Admin_Role_Id),
                ResourceId = Ulid.Parse(RoleConstes.Get_Id_Resource_Id)
            },
            new Permission
            {
                Id = Ulid.Parse(RoleConstes.Get_List_Filter_Permission_Id),
                RoleId =Ulid.Parse(RoleConstes.Admin_Role_Id),
                ResourceId = Ulid.Parse(RoleConstes.Get_List_Filter_Resource_Id)
            },
            #endregion Role Resources Permissions
        ];
    }
}