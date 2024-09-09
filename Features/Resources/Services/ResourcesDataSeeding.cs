using Auth.Domain.Entities;
using Auth.Shared.Constes;

namespace Auth.Features.Resources.Services
{
    public static class ResourcesDataSeeding
    {
        public static IEnumerable<Resource> InitialItems =>
        [
            #region  Auth User Resources
               new Resource
                {
                    Id = Ulid.Parse(UserConstes.Create_Resource_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    GroupId = Ulid.Parse(ResourceGroupConstes.Auth_User_Group_Id),
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
                    GroupId = Ulid.Parse(ResourceGroupConstes.Auth_User_Group_Id),
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
                    GroupId = Ulid.Parse(ResourceGroupConstes.Auth_User_Group_Id),
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
                    GroupId = Ulid.Parse(ResourceGroupConstes.Auth_User_Group_Id),
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
                    GroupId = Ulid.Parse(ResourceGroupConstes.Auth_User_Group_Id),
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
                    GroupId = Ulid.Parse(ResourceGroupConstes.Auth_User_Group_Id),
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
                    GroupId = Ulid.Parse(ResourceGroupConstes.Auth_User_Group_Id),
                    Title = UserConstes.Get_List_Filter_Resource_Title,
                    Url = UserConstes.Get_List_Filter_Resource_Url,
                    Method = HttpMethod.Get.ToString(),
                    IsPublic = false,
                    RequirePermission = true
                },
                new Resource
                {
                    Id = Ulid.Parse(UserConstes.Update_Profile_Resource_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    GroupId = Ulid.Parse(ResourceGroupConstes.Auth_User_Group_Id),
                    Title = UserConstes.Update_Profile_Resource_Title,
                    Url = UserConstes.Update_Profile_Resource_Url,
                    Method = HttpMethod.Put.ToString(),
                    IsPublic = false,
                    RequirePermission = false
                },
                new Resource
                {
                    Id = Ulid.Parse(UserConstes.Get_Profile_Resource_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    GroupId = Ulid.Parse(ResourceGroupConstes.Auth_User_Group_Id),
                    Title = UserConstes.Get_Profile_Resource_Title,
                    Url = UserConstes.Get_Profile_Resource_Url,
                    Method = HttpMethod.Get.ToString(),
                    IsPublic = false,
                    RequirePermission = false
                },
                new Resource
                {
                    Id = Ulid.Parse(UserConstes.Login_Resource_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    GroupId = Ulid.Parse(ResourceGroupConstes.Auth_User_Group_Id),
                    Title = UserConstes.Login_Resource_Title,
                    Url = UserConstes.Login_Resource_Url,
                    Method = HttpMethod.Post.ToString(),
                    IsPublic = true,
                    RequirePermission = false
                },
            #endregion Auth Users Resources

            #region  Auth Organization Resources
               new Resource
                {
                    Id = Ulid.Parse(OrganizationConstes.Create_Resource_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    GroupId = Ulid.Parse(ResourceGroupConstes.Auth_Organization_Group_Id),
                    Title = OrganizationConstes.Create_Resource_Title,
                    Url = OrganizationConstes.Create_Resource_Url,
                    Method = HttpMethod.Post.ToString(),
                    IsPublic = false,
                    RequirePermission = true,
                },
                new Resource
                {
                    Id = Ulid.Parse(OrganizationConstes.Update_Resource_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    GroupId = Ulid.Parse(ResourceGroupConstes.Auth_Organization_Group_Id),
                    Title = OrganizationConstes.Create_Resource_Title,
                    Url = OrganizationConstes.Update_Resource_Url,
                    Method = HttpMethod.Put.ToString(),
                    IsPublic = false,
                    RequirePermission = true
                },
                new Resource
                {
                    Id = Ulid.Parse(OrganizationConstes.Delete_Resource_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    GroupId = Ulid.Parse(ResourceGroupConstes.Auth_Organization_Group_Id),
                    Title = OrganizationConstes.Delete_Resource_Title,
                    Url = OrganizationConstes.Delete_Resource_Url,
                    Method = HttpMethod.Delete.ToString(),
                    IsPublic = false,
                    RequirePermission = true
                },
                new Resource
                {
                    Id = Ulid.Parse(OrganizationConstes.Get_Id_Resource_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    GroupId = Ulid.Parse(ResourceGroupConstes.Auth_Organization_Group_Id),
                    Title = OrganizationConstes.Get_Id_Resource_Title,
                    Url = OrganizationConstes.Get_Id_Resource_Url,
                    Method = HttpMethod.Get.ToString(),
                    IsPublic = false,
                    RequirePermission = true
                },
                new Resource
                {
                    Id = Ulid.Parse(OrganizationConstes.Get_List_Filter_Resource_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    GroupId = Ulid.Parse(ResourceGroupConstes.Auth_Organization_Group_Id),
                    Title = OrganizationConstes.Get_List_Filter_Resource_Title,
                    Url = OrganizationConstes.Get_List_Filter_Resource_Url,
                    Method = HttpMethod.Get.ToString(),
                    IsPublic = false,
                    RequirePermission = true
                },
                #endregion Auth.Users
            ];
    }
}