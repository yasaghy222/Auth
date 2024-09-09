using ErrorOr;
namespace Auth.Shared.CustomErrors;

public static class OrganizationErrors
{
    public const string CreateLogMsg = "Organization_Create_Error: some error has occurred when adding organization with this input data: {Errors}";
    public const string ChangePasswordLogMsg = "Organization_ChangePassword_Error: some error has occurred when changing organization password with this input data: {Errors}";
    public const string DeleteLogMsg = "Organization_Delete_Error: some error has occurred when deleting organization with this input data: {Errors}";
    public const string GetBydLogMsg = "Organization_GetByd_Error: some error has occurred when get organization by Id with this input data: {Errors}";
    public const string UpdateLogMsg = "Organization_Update_Error: some error has occurred when updating organization with this input data: {Errors}";
    public const string ChangeStatusLogMsg = "Organization_ChangeStatus_Error: some error has occurred when changing organization status with this input data: {Errors}";


    public static Error DuplicateTitle(string title) =>
        Error.Validation("Organization.DuplicateTitle",
        $"your input Organization Title:'{title}' already is exist.");

    public static Error NotFound() => Error.NotFound("Organization.NotFound",
        $"there are no organization with this data exist in the database.");

    public static Error SuspendStatus() => Error.NotFound("Organization.SuspendStatus",
     $"this organization was suspend plz talk with admin");
}
