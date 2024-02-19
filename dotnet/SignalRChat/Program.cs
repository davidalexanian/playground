using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.DependencyInjection;
using SignalRChat.Hubs;

namespace SignalRChat
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
                .AddJsonProtocol(config => {
                    config.PayloadSerializerOptions.WriteIndented = true;
                    config.PayloadSerializerOptions.AllowTrailingCommas = true;
                })
                .AddMessagePackProtocol(config => { })
                .AddHubOptions<ClockHub>(config => {
                    config.EnableDetailedErrors = true;
                    config.SupportedProtocols = new List<string>() { "json" };
                })
                .AddHubOptions<ChatHub>(config => {
                    config.EnableDetailedErrors = true;
                    config.SupportedProtocols = new List<string>() { "messagepack" };
                });

            builder.Services.AddCors(options =>
                options.AddDefaultPolicy(builder => builder
                    .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()));

            builder.Services.AddHostedService<Worker>();


            var app = builder.Build();
            //app.UseResponseCompression();

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
            app.MapHub<ChatHub>("/chatHub", options => {
                options.Transports = HttpTransportType.WebSockets;
                options.ApplicationMaxBufferSize = 64 * 1024;   // 64kb default
                options.TransportMaxBufferSize = 64 * 1024;     // 64kb default
            });
            app.MapHub<ClockHub>("/clockHub");

            app.Run();
        }
    }
}