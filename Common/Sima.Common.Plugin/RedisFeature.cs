using System.Collections.Generic;
using ServiceStack;
using ServiceStack.Redis;

namespace Sima.Common.Plugin
{
    public class RedisFeature : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            var container = appHost.GetContainer();
            var appSettings = appHost.AppSettings;
            var connections = appSettings.Get<Dictionary<string, string>>("connectionStrings");
#if DEBUG
            var sentinelHosts = new[] { connections.GetValueOrDefault("Sentinel0"), connections.GetValueOrDefault("Sentinel1"), connections.GetValueOrDefault("Sentinel2") };
            var sentinel = new RedisSentinel(sentinelHosts, masterName: appSettings.GetString("redis.mastername"))
            {
                RedisManagerFactory = (master, slaves) => new RedisManagerPool(master, new RedisPoolConfig()
                {
                    MaxPoolSize = 20
                }),
                HostFilter = host => "{0}?db=0".Fmt(host)
            };
            container.Register<IRedisClientsManager>(c => sentinel.Start());
#else
            var redisManager = new RedisManagerPool(connections.GetValueOrDefault("Sentinel0"), new RedisPoolConfig() {
                MaxPoolSize = 20,
            });
            container.Register<IRedisClientsManager>(c => redisManager);
#endif
        }
    }
}