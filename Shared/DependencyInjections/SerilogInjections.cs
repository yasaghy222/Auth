using Serilog;

namespace Auth.Shared.DependencyInjections
{
    public static class SerilogInjections
    {
        public static IServiceCollection AddSerilog(this WebApplicationBuilder builder)
        {
            string? defSeqUrl = builder.Configuration["BaseUrl:Seq"];
            string? seqUrl = Environment.GetEnvironmentVariable("BaseUrl_Seq") ?? defSeqUrl;

            builder.Host.UseSerilog((context, loggerConfig) =>
            {
                loggerConfig.ReadFrom.Configuration(context.Configuration)
                .WriteTo.Seq(seqUrl ?? "")
                .WriteTo.Console();
            });

            return builder.Services;
        }
    }
}