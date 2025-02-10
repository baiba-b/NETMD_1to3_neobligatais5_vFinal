using bb23028_MD1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;


namespace bb23028MD2
{
    public static class MauiProgram
    {
        public static IConfiguration Configuration { get; set; }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange:true)
                .Build();
            builder.Configuration.AddConfiguration(config);
            builder.Services.TryAddSingleton<IConfiguration>(config);
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<AssignmentListView>();
            builder.Services.AddTransient<CreateAssignment>();
            builder.Services.AddTransient<CreateStudent>();
            builder.Services.AddTransient<CreateSubmission>();
            builder.Services.AddTransient<StudentListView>();
            builder.Services.AddTransient<SubmissionListView>();
            builder.Services.AddTransient<UniversityComponents>();




#if DEBUG
            builder.Logging.AddDebug();
#endif          

            return builder.Build();
        }
    }
}
