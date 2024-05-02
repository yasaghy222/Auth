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
    }
}