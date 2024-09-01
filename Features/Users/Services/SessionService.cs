using LanguageExt;
using StackExchange.Redis;
using Microsoft.IdentityModel.Tokens;
using LanguageExt.UnsafeValueAccess;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.Contracts.Responses;

namespace Auth.Features.Users.Services
{
    public interface ISessionService
    {
        public Task CreateAsync(CreateSessionRequest request);

        public Task<SessionsResponse> GetListByUserIdAsync(Ulid userId);

        public Task<bool> DeleteSessionAsync(Ulid userId, string sessionId);
        public Task<bool> DeleteSessionAsync(Ulid userId, Func<SessionResponse, bool> condition);
    }

    public sealed class SessionService(IConnectionMultiplexer redis)
        : ISessionService
    {
        private readonly IConnectionMultiplexer _redis = redis;
        private readonly IDatabase _redisDatabase = redis.GetDatabase();

        public async Task CreateAsync(CreateSessionRequest request)
        {
            string sessionKey = $"session:{request.Id}";
            await _redisDatabase.HashSetAsync(sessionKey, request.MapToHashEntry());

            string userSessionsKey = $"user_sessions:{request.UserId}";
            await _redisDatabase.SetAddAsync(userSessionsKey, request.Id.ToString());
        }

        public async Task<Option<SessionResponse>> GetSessionsAsync(string? id)
        {
            if (id.IsNullOrEmpty())
            {
                return Option<SessionResponse>.None;
            }

            string sessionKey = $"session:{id}";
            HashEntry[] hashEntries = await _redisDatabase.HashGetAllAsync(sessionKey);
            if (hashEntries.Length == 0)
            {
                return Option<SessionResponse>.None;
            }

            return hashEntries.MapToResponse(id ?? string.Empty);
        }

        public async Task<SessionsResponse> GetListByUserIdAsync(Ulid userId)
        {
            string userSessionsKey = $"user_sessions:{userId}";
            RedisValue[] sessionIds = await _redisDatabase.SetMembersAsync(userSessionsKey);

            IEnumerable<Task<Option<SessionResponse>>> tasks = sessionIds
                .Select(async id => await GetSessionsAsync(id));

            Option<SessionResponse>[] results = await Task.WhenAll(tasks);
            SessionsResponse sessions = new()
            {
                Items = results.Where(i => i.IsSome).Select(i => i.ValueUnsafe())
            };

            return sessions;
        }

        public async Task<bool> DeleteSessionAsync(Ulid userId, string sessionId)
        {
            string sessionKey = $"session:{sessionId}";
            bool isSessionDel = await _redisDatabase.KeyDeleteAsync(sessionKey);

            if (!isSessionDel)
            {
                return false;
            }

            string userSessionsKey = $"user_sessions:{userId}";
            bool isDelFromList = await _redisDatabase.SetRemoveAsync(userSessionsKey, sessionId);

            return isDelFromList;
        }

        public async Task<bool> DeleteSessionAsync(Ulid userId, Func<SessionResponse, bool> condition)
        {
            SessionsResponse userSessions = await GetListByUserIdAsync(userId);

            SessionResponse? session = userSessions.Items.FirstOrDefault(condition);
            if (session == null)
            {
                return false;
            }

            return await DeleteSessionAsync(userId, session.Id);
        }


        public async Task SetSessionExpirationAsync(string sessionId, TimeSpan expiration)
        {
            string sessionKey = $"session:{sessionId}";
            await _redisDatabase.KeyExpireAsync(sessionKey, expiration);
        }
    }
}