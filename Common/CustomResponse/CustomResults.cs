using Authenticate.Models;

namespace Authenticate.Common
{
    public class CustomResults
    {
        public static Result UserCreated(object data) => new()
        {
            Message = new()
            {
                Fa = "عملیات ایجاد کاربر با موفقیت انجام شد",
                En = "Create User Done"
            },
            StatusCode = StatusCodes.Status201Created,
            Data = data
        };
        
        public static Result TokenGenerated(object data) => new()
        {
            Message = new()
            {
                Fa = "عملیات ساخت توکن با موفقیت انجام شد",
                En = "َGenerate Toke Done"
            },
            StatusCode = StatusCodes.Status201Created,
            Data = data
        };

        public static Result OrganizationUpdated() => new()
        {
            Message = new()
            {
                Fa = "ویرایش سازمان با موفقیت به انجام شد",
                En = "Update Organization Done"
            },
            StatusCode = StatusCodes.Status200OK,
        };

        public static Result OrganizationAdd(object data) => new()
        {
            Message = new()
            {
                Fa = "افزودن سازمان با موفقیت به انجام شد",
                En = "Update Organization Done"
            },
            StatusCode = StatusCodes.Status200OK,
            Data = data
        };


        public static Result OrganizationFounded(object data) => new()
        {
            Message = new()
            {
                Fa = "عملیات واکشی رکورد با موفقیت انجام شد",
                En = "Organization Founded"
            },
            StatusCode = StatusCodes.Status200OK,
            Data = data
        };
    }
}