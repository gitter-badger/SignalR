// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Redis;
using StackExchange.Redis;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RedisDependencyInjectionExtensions
    {
        public static ISignalRServerBuilder AddRedis(this ISignalRServerBuilder signalrBuilder)
        {
            return AddRedis(signalrBuilder, o => { });
        }

        public static ISignalRServerBuilder AddRedis(this ISignalRServerBuilder signalrBuilder, string redisConnectionString)
        {
            return AddRedis(signalrBuilder, o =>
            {
                o.Configuration = ConfigurationOptions.Parse(redisConnectionString);
            });
        }

        public static ISignalRServerBuilder AddRedis(this ISignalRServerBuilder signalrBuilder, Action<RedisOptions> configure)
        {
            signalrBuilder.Services.Configure(configure);
            signalrBuilder.Services.AddSingleton(typeof(HubLifetimeManager<>), typeof(RedisHubLifetimeManager<>));
            return signalrBuilder;
        }

        public static ISignalRServerBuilder AddRedis(this ISignalRServerBuilder signalrBuilder, string redisConnectionString, Action<RedisOptions> configure)
        {
            return AddRedis(signalrBuilder, o =>
            {
                o.Configuration = ConfigurationOptions.Parse(redisConnectionString);
                configure(o);
            });
        }
    }
}
