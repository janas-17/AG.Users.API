﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AG.Users.API.HealthChecks
{
    public static class GCInfoHealthCheckBuilderExtensions
    {
        public static IHealthChecksBuilder AddMemoryHealthCheck(
            this IHealthChecksBuilder builder,
            string name,
            HealthStatus? failureStatus = null,
            IEnumerable<string> tags = null,
            long? thresholdInBytes = null)
        {
            // Register a check of type GCInfo.
            builder.AddCheck<MemoryHealthCheck>(
                name, failureStatus ?? HealthStatus.Degraded, tags);

            // Configure named options to pass the threshold into the check.
            if (thresholdInBytes.HasValue)
            {
                builder.Services.Configure<MemoryCheckOptions>(name, options =>
                {
                    options.Threshold = thresholdInBytes.Value;
                });
            }

            return builder;
        }
    }
}
