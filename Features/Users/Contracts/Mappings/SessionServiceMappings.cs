using Auth.Features.Users.Contracts.Enums;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Responses;
using StackExchange.Redis;

namespace Auth.Features.Users.Contracts.Mappings
{
    public static class SessionServiceMappings
    {
        public static HashEntry[] MapToHashEntry(this CreateSessionRequest request)
        {
            return [
                new HashEntry(SessionFields.UserId, request.UserId.ToString()),
                new HashEntry(SessionFields.Platform, request.Platform.ToString()),
                new HashEntry(SessionFields.UniqueId, request.UniqueId.ToString()),
                new HashEntry(SessionFields.IP, request.IP.ToString()),
                new HashEntry(SessionFields.OrganizationId, request.OrganizationId.ToString()),
                new HashEntry(SessionFields.OrganizationId, request.OrganizationTitle.ToString()),
                new HashEntry(SessionFields.ExpireAt, request.ExpireAt.ToString()),
            ];
        }

        public static string GetFiledValue(this HashEntry[] sessionData, string fieldName)
        {
            HashEntry filed = sessionData.FirstOrDefault(x => x.Name == fieldName);
            return filed.Value.ToString();
        }


        public static SessionResponse MapToResponse(this HashEntry[] sessionData)
        {
            return new()
            {
                Platform = Enum.Parse<SessionPlatform>(sessionData.GetFiledValue(SessionFields.Platform)),

                IP = sessionData.GetFiledValue(SessionFields.IP),
                UniqueId = sessionData.GetFiledValue(SessionFields.UniqueId),

                OrganizationId = Ulid.Parse(sessionData.GetFiledValue(SessionFields.OrganizationId)),
                OrganizationTitle = sessionData.GetFiledValue(SessionFields.OrganizationTitle),

                ExpireAt = DateTime.Parse(sessionData.GetFiledValue(SessionFields.ExpireAt)),
            };
        }
    }
}