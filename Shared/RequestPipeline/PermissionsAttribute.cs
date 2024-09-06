using Microsoft.AspNetCore.Mvc.Filters;

namespace Auth.Shared.RequestPipeline
{
    // [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PermissionsAttribute : Attribute
    {
        public string[] _requiredPermissions { get; init; } = [];

        public PermissionsAttribute(string requiredPermission)
        {
            _requiredPermissions = [requiredPermission];
        }

        public PermissionsAttribute(string[] requiredPermissions)
        {
            _requiredPermissions = requiredPermissions;
        }
    }
}
