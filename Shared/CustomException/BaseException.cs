namespace Auth.Shared.CustomException
{
    public class BaseException(int status, string title, string type, string detail) : Exception
    {
        public int Status { get; } = status;
        public string Title { get; } = title;
        public string Type { get; } = type;
        public string Detail { get; } = detail;
    }
}