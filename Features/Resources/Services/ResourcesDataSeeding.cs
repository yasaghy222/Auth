using Auth.Domain.Entities;
using Auth.Shared.Constes;

namespace Auth.Features.Resources.Services
{
    public static class ResourcesDataSeeding
    {
        public static IEnumerable<Resource> InitialItems =>
        [
            #region  Auth.Users
               new Resource
                {
                    Id = Ulid.Parse(UserConstes.Create_Resource_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    GroupId = Ulid.Parse(ResourceGroupConstes.User_Group_Id),
                    Title = UserConstes.Create_Resource_Title,
                    Url = UserConstes.Create_Resource_Url,
                    Method = HttpMethod.Post.ToString(),
                    IsPublic = false,
                    RequirePermission = true,
                },
                new Resource
                {
                    Id = Ulid.Parse(UserConstes.Update_Resource_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    GroupId = Ulid.Parse(ResourceGroupConstes.User_Group_Id),
                    Title = UserConstes.Create_Resource_Title,
                    Url = UserConstes.Update_Resource_Url,
                    Method = HttpMethod.Put.ToString(),
                    IsPublic = false,
                    RequirePermission = true
                },
                new Resource
                {
                    Id = Ulid.Parse(UserConstes.Delete_Resource_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    GroupId = Ulid.Parse(ResourceGroupConstes.User_Group_Id),
                    Title = UserConstes.Delete_Resource_Title,
                    Url = UserConstes.Delete_Resource_Url,
                    Method = HttpMethod.Delete.ToString(),
                    IsPublic = false,
                    RequirePermission = true
                },
                new Resource
                {
                    Id = Ulid.Parse(UserConstes.Change_Password_Resource_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    GroupId = Ulid.Parse(ResourceGroupConstes.User_Group_Id),
                    Title = UserConstes.Change_Password_Resource_Title,
                    Url = UserConstes.Change_Password_Resource_Url,
                    Method = HttpMethod.Patch.ToString(),
                    IsPublic = false,
                    RequirePermission = true
                },
                new Resource
                {
                    Id = Ulid.Parse(UserConstes.Reset_Password_Resource_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    GroupId = Ulid.Parse(ResourceGroupConstes.User_Group_Id),
                    Title = UserConstes.Reset_Password_Resource_Title,
                    Url = UserConstes.Reset_Password_Resource_Url,
                    Method = HttpMethod.Patch.ToString(),
                    IsPublic = true,
                    RequirePermission = false
                },
                new Resource
                {
                    Id = Ulid.Parse(UserConstes.Get_Id_Resource_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    GroupId = Ulid.Parse(ResourceGroupConstes.User_Group_Id),
                    Title = UserConstes.Get_Id_Resource_Title,
                    Url = UserConstes.Get_Id_Resource_Url,
                    Method = HttpMethod.Get.ToString(),
                    IsPublic = false,
                    RequirePermission = true
                },
                new Resource
                {
                    Id = Ulid.Parse(UserConstes.Get_List_Filter_Resource_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    GroupId = Ulid.Parse(ResourceGroupConstes.User_Group_Id),
                    Title = UserConstes.Get_List_Filter_Resource_Title,
                    Url = UserConstes.Get_List_Filter_Resource_Url,
                    Method = HttpMethod.Get.ToString(),
                    IsPublic = false,
                    RequirePermission = true
                },
                #endregion Auth.Users
            ];
    }
}