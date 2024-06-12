using LivestockManagementSystem.ViewModels;
using LivestockManagementSystem.Views;
using Microsoft.Extensions.Logging;

namespace LivestockManagementSystem
{
    public static class MauiProgram
    {
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<DashboardView>();
            builder.Services.AddSingleton<ManagementViewModel>();
            builder.Services.AddTransient<ManagementView>();
            builder.Services.AddSingleton<FinanceViewModel>();
            builder.Services.AddTransient<FinanceView>();
            return builder.Build();
        }
    }
}
