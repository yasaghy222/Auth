using Auth.Domain.Entities;
using Auth.Shared.Constes;

namespace Auth.Features.Resources.Services
{
    public static class ResourceGroupsDataSeeding
    {
        public static IEnumerable<ResourceGroup> InitialItems =>
        [
               new ResourceGroup
                {
                    Id = Ulid.Parse(ResourceGroupConstes.Auth_User_Group_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    Title = ResourceGroupConstes.Auth_User_Group_Title,
                    Order = 1,
                },
                new ResourceGroup
                {
                    Id = Ulid.Parse(ResourceGroupConstes.Auth_Organization_Group_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Accounting_Service_Id),
                    Title = ResourceGroupConstes.Auth_Organization_Group_Title,
                    Order = 2,
                },
                new ResourceGroup
                {
                    Id = Ulid.Parse(ResourceGroupConstes.Auth_Role_Group_Id),
                    OrganizationId = Ulid.Parse(OrganizationConstes.Accounting_Service_Id),
                    Title = ResourceGroupConstes.Auth_Role_Group_Title,
                    Order = 3,
                },
        ];
    }
}