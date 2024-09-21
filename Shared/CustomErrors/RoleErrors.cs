using ErrorOr;
namespace Auth.Shared.CustomErrors;

public static class RoleErrors
{
    public const string CreateLogMsg = "Role_Create_Error: some error has occurred when adding role with this input data: {Errors}";
    public const string ChangePasswordLogMsg = "Role_ChangePassword_Error: some error has occurred when changing role password with this input data: {Errors}";
    public const string DeleteLogMsg = "Role_Delete_Error: some error has occurred when deleting role with this input data: {Errors}";
    public const string GetByFilterLogMsg = "Role_GetByFilter_Error: some error has occurred when get role by filter with this input data: {Errors}";
    public const string UpdateLogMsg = "Role_Update_Error: some error has occurred when updating role with this input data: {Errors}";
    public const string ChangeStatusLogMsg = "Role_ChangeStatus_Error: some error has occurred when changing role status with this input data: {Errors}";

    public static Error CanNotDelInitialItem() => Error.Validation("Role.CanNotDelInitialItem",
          $"you can not delete initial role item");

    public static Error DuplicateTitle(string title) =>
        Error.Validation("Role.DuplicateTitle",
        $"your input Role Title:'{title}' already is exist.");

    public static Error NotFound() => Error.NotFound("Role.NotFound",
        $"there are no role with this data exist in the database.");

    public static Error SuspendStatus() => Error.NotFound("Role.SuspendStatus",
     $"this role was suspend plz talk with admin");
}
