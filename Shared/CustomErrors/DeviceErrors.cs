using ErrorOr;
namespace Auth.Shared.CustomErrors;

public static class UserErrors
{
    public const string CreateLogMsg = "User_Create_Error: some error has occurred when adding user with this input data: {Errors}";
    public const string ChangePasswordLogMsg = "User_ChangePassword_Error: some error has occurred when changing user password with this input data: {Errors}";
    public const string DeleteLogMsg = "User_Delete_Error: some error has occurred when deleting user with this input data: {Errors}";
    public const string GetBydLogMsg = "User_GetByd_Error: some error has occurred when get user by Id with this input data: {Errors}";
    public const string UpdateLogMsg = "User_Update_Error: some error has occurred when updating user with this input data: {Errors}";
    public const string ChangeStatusLogMsg = "User_ChangeStatus_Error: some error has occurred when changing user status with this input data: {Errors}";
    public const string GetByOrganizationLogMsg = "User_GetByOrganizationId_Error: some error has occurred when get user by branchId with this input data: {Errors}";


    public static Error DuplicateUsername(string username) =>
        Error.Validation("User.DuplicateUsername",
        $"your input Username:'{username}' already is exist.");

    public static Error DuplicatePhone(string phone) =>
        Error.Validation("User.DuplicatePhone",
        $"your input Phone:'{phone}' already is exist.");

    public static Error RepeatedPassword() =>
         Error.Validation("User.RepeatedPassword",
         $"plz choose the new password.");


    public static Error NotFound() => Error.NotFound("User.NotFound",
        $"there are no user with this data exist in the database.");

    public static Error OrganizationNotFound() => Error.NotFound("User.Organization.NotFound",
        $"there are no organization with this data exist in the database.");

    public static Error InvalidUserPass() => Error.NotFound("User.InvalidUserPass",
        $"your username or password are invalid");

    public static Error SuspendStatus() => Error.NotFound("User.SuspendStatus",
     $"your account was suspend plz talk with admin");

    public static Error BlockStatus() => Error.NotFound("User.BlockStatus",
        @"you are block for so many time try with invalid username or password,
        plz wait for 5 minute and try again with valid data");
}
