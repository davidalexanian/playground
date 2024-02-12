using SignalRChat.Hubs;

namespace SignalRChat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services
                .AddSignalR(hubOptions => {
                    hubOptions.EnableDetailedErrors = true;
                })
                .AddJsonProtocol(config => {
                    config.PayloadSerializerOptions.WriteIndented = true;
                    config.PayloadSerializerOptions.AllowTrailingCommas = true;
                });

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("https://example.com")
                            .AllowAnyHeader()
                            .WithMethods("GET", "POST")
                            .AllowCredentials();
                    });
            });

            var app = builder.Build();

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
            app.MapHub<ChatHub>("/chatHub");

            app.Run();
        }
    }
}