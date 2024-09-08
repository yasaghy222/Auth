using Auth.Domain.Entities;
using Auth.Shared.Constes;
using Auth.Features.Organizations.Contracts.Enums;

namespace Auth.Features.Organizations.Services
{
    public static class OrganizationDataSeeding
    {
        public static IEnumerable<Organization> InitialItems =>
         [
                new Organization
                {
                    Id = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                    Title = OrganizationConstes.Auth_Service_Title,
                    Status = OrganizationStatus.Active,
                    ParentId = null,
                },
                new Organization
                {
                    Id = Ulid.Parse(OrganizationConstes.Accounting_Service_Id),
                    Title = OrganizationConstes.Accounting_Service_Title,
                    Status = OrganizationStatus.Active,
                    ParentId = Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                },
                new Organization
                {
                    Id = Ulid.Parse(OrganizationConstes.RedSense_Service_Id),
                    Title = OrganizationConstes.RedSense_Service_Title,
                    Status = OrganizationStatus.Active,
                    ParentId = Ulid.Parse(OrganizationConstes.Auth_Service_Id)
                },
                new Organization
                {
                    Id = Ulid.Parse(OrganizationConstes.RedGuard_Update_Service_Id),
                    Title = OrganizationConstes.RedGuard_Update_Service_Title,
                    Status = OrganizationStatus.Active,
                    ParentId = Ulid.Parse(OrganizationConstes.Auth_Service_Id)
                }
            ];
    }
}