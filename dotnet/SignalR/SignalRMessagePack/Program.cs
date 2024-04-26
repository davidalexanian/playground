using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Http.Connections;
using SignalRMessagePack.Hubs;

namespace SignalRMessagePack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddResponseCompression(options => options.EnableForHttps = true);

            builder.Services
                .AddSignalR()
                .AddNewtonsoftJsonProtocol(config => {
                    //config.PayloadSerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.RoundtripKind; default
                    config.PayloadSerializerSettings.DateParseHandling = Newtonsoft.Json.DateParseHandling.DateTimeOffset;
                })
                .AddMessagePackProtocol(config => {
                    config.SerializerOptions =
                        StandardResolver.Options
                            .WithAllowAssemblyVersionMismatch(true)
                            .WithSecurity(MessagePackSecurity.UntrustedData)
                            .WithResolver(CompositeResolver.Create(
                                new IMessagePackFormatter[] { 
                                    MyNullableDecimalFormatter.Instance,
                                    MyDecimalFormatter.Instance,
                                    MyDateTimeFormatter.Instance,
                                    MyNullableDateTimeFormatter.Instance,
                                    MyDateTimeOffsetFormatter.Instance,
                                    MyNullableDateTimeOffsetFormatter.Instance,
                                    JObjectFormatter.Instance,
                                    JArrayFormatter.Instance
                                },
                                new IFormatterResolver[] { StandardResolver.Instance }));
                })
                .AddHubOptions<JsonHub>(config => {
                    config.EnableDetailedErrors = true;
                    config.SupportedProtocols = new List<string>() { "json" };
                })
                .AddHubOptions<MessagePackHub>(config => {
                    config.EnableDetailedErrors = true;
                    config.SupportedProtocols = new List<string>() { "messagepack" };
                });

            builder.Services.AddCors(options =>
                options.AddDefaultPolicy(builder => builder
                    .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()));


            var app = builder.Build();
            app.UseResponseCompression();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();

            app.UseCors();  // UseCors must be called before MapHub.
            app.MapHub<MessagePackHub>("/messagePackHub", options => {
                options.Transports = HttpTransportType.WebSockets;
                options.ApplicationMaxBufferSize = 64 * 1024;   // 64kb default
                options.TransportMaxBufferSize = 64 * 1024;     // 64kb default
            });
            app.MapHub<JsonHub>("/jsonHub", options => {
                options.Transports = HttpTransportType.WebSockets;
                options.ApplicationMaxBufferSize = 64 * 1024;   // 64kb default
                options.TransportMaxBufferSize = 64 * 1024;     // 64kb default
            });

            app.Run();
        }
    }
}