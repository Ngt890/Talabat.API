using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class ResponseCasheService : IResponseCasheService
    {
        private IDatabase _database;
        public ResponseCasheService(IConnectionMultiplexer Redis)
        {
            _database=Redis.GetDatabase();
        }
        public async Task CasheResponseAsync(string CashKey, object Resonse, TimeSpan TimetoLive)
        {
            if (Resonse is null) return;
            var serializingoptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var SerializedResponse=JsonSerializer.Serialize(Resonse);
            await _database.StringSetAsync(CashKey,SerializedResponse,TimetoLive);
        }

        public async Task<string ?> GetCashedData(string CashKey)
        {
            var CashedData= await _database.StringGetAsync(CashKey);
            if (CashedData.IsNullOrEmpty) return null;
            return CashedData;
           
        }
    }
}
