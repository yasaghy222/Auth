using Authenticate.Models;

namespace Authenticate.Common
{
    public class CustomErrors
    {
        public static Result InvalidUserData(object errors) => new()
        {
            Message = new()
            {
                Fa = "دیتای ورودی معتبر نمی باشد",
                En = "Data is Invalid!"
            },
            StatusCode = StatusCodes.Status400BadRequest,
            Data = errors,
            Status = false
        };

        public static Result InvalidOrganizationData(object errors) => new()
        {
            Message = new()
            {
                Fa = "دیتای ورودی معتبر نمی باشد",
                En = "Data is Invalid!"
            },
            StatusCode = StatusCodes.Status400BadRequest,
            Data = errors,
            Status = false
        };


        public static Result CreateUserFailed() => new()
        {
            Message = new()
            {
                Fa = "عملیات ایجاد کاربر با مشکل مواجه شد",
                En = "Create User Failed!"
            },
            StatusCode = StatusCodes.Status500InternalServerError,
            Status = false
        };

        public static Result InvalidUsernameOrPassword() => new()
        {
            Message = new()
            {
                Fa = "نام کاربری یا رمز عبور اشتباه می باشد",
                En = "َUsername or Password is Wrong!"
            },
            StatusCode = StatusCodes.Status400BadRequest,
            Status = false
        };

        public static Result UsernameAlreadyExist(object data, int? code = null) => new()
        {
            Message = new()
            {
                Fa = "ایجاد حساب کاربری برای نام کاربری وارد شده قبلا انجام شده است.",
                En = "Username Already Existg!"
            },
            StatusCode = StatusCodes.Status400BadRequest,
            Data = data,
            Code = code,
            Status = false
        };

        public static Result GenerateTokenFailed() => new()
        {
            Message = new()
            {
                Fa = "عملیات ساخت توکن با مشکل مواجه شد",
                En = "َGenerate Toke Failed!"
            },
            StatusCode = StatusCodes.Status500InternalServerError,
            Status = false
        };

        public static Result OrganizationNotFound() => new()
        {
            Message = new()
            {
                Fa = "سیستم موردنظر یافت نشد",
                En = "System Not Found!"
            },
            StatusCode = StatusCodes.Status400BadRequest,
            Status = false
        };

        public static Result OrganizationQueryFailed(object? data = null) => new()
        {
            Message = new()
            {
                Fa = "عملیات واکشی سازمان ها با مشکل مواجه شد",
                En = "Organization Query Failed!"
            },
            Data = data,
            StatusCode = StatusCodes.Status500InternalServerError,
            Status = false
        };

        public static Result OrganizationUpdateFailed() => new()
        {
            Message = new()
            {
                Fa = "عملیات ویرایش سازمان با مشکل مواجه شد",
                En = "Organization Update Failed!"
            },
            StatusCode = StatusCodes.Status500InternalServerError,
            Status = false
        };

        public static Result OrganizationAddFailed(object? data = null) => new()
        {
            Message = new()
            {
                Fa = "عملیات افزودن سازمان با مشکل مواجه شد",
                En = "Organization Update Failed!"
            },
            Data = data,
            StatusCode = StatusCodes.Status500InternalServerError,
            Status = false
        };

        public static Result InternalServer(object? errors = null) => new()
        {
            Message = new()
            {
                Fa = "با عرض پوزش، عملیات درخواستی با مشکل مواجه شد!",
                En = "Operation Failed!"
            },
            StatusCode = StatusCodes.Status500InternalServerError,
            Data = errors,
            Status = false
        };
    }
}
