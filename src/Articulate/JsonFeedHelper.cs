﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Umbraco.Core.Cache;

namespace Articulate
{
    public static class JsonFeedHelper
    {
        public static JArray GetResult(AppCaches cache, string url)
        {
            return (JArray)cache.RuntimeCache.Get(url, () =>
            {
                using (var client = new HttpClient())
                {
                    var result = client.GetStringAsync(url);
                    Task.WaitAll(result);
                    return JsonConvert.DeserializeObject<JArray>(result.Result);
                }
            });
        }
    }
}