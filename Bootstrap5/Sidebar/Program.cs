using System.Globalization;

namespace Sidebar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //↓役割謎。無くても機能しているような・・・。
            //builder.Services.AddLocalization(options => options.ResourcesPath = "MyProjectResources");
            builder.Services.AddHttpContextAccessor();
            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();
            var supportedCultures = new List<CultureInfo> { new CultureInfo("ja-JP") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                //DefaultRequestCulture = new RequestCulture("ja-JP"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}