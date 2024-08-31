using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Responses;
using StackExchange.Redis;

namespace Auth.Features.Users.Services
{
    public interface ISessionService
    {
        public Task CreateAsync(CreateSessionRequest request);
        public Task<SessionsResponse> GetListByUserIdAsync(Ulid userId);
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

        public async Task<SessionResponse> GetSessionsAsync(string? id)
        {
            string sessionKey = $"session:{id}";
            HashEntry[] hashEntries = await _redisDatabase.HashGetAllAsync(sessionKey);
            return hashEntries.MapToResponse();
        }

        public async Task<SessionsResponse> GetListByUserIdAsync(Ulid userId)
        {
            string userSessionsKey = $"user_sessions:{userId}";
            RedisValue[] sessionIds = await _redisDatabase.SetMembersAsync(userSessionsKey);

            IEnumerable<Task<SessionResponse>> tasks = sessionIds
                .Select(async id => await GetSessionsAsync(id));

            SessionResponse[] results = await Task.WhenAll(tasks);

            SessionsResponse sessions = new()
            {
                Items = results
            };

            return sessions;
        }


        public async Task DeleteSessionAsync(string sessionId, string userId)
        {
            string sessionKey = $"session:{sessionId}";
            await _redisDatabase.KeyDeleteAsync(sessionKey);

            string userSessionsKey = $"user_sessions:{userId}";
            await _redisDatabase.SetRemoveAsync(userSessionsKey, sessionId);
        }

        public async Task SetSessionExpirationAsync(string sessionId, TimeSpan expiration)
        {
            string sessionKey = $"session:{sessionId}";
            await _redisDatabase.KeyExpireAsync(sessionKey, expiration);
        }
    }
}