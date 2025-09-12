using Microsoft.Extensions.Logging;

namespace MauiHybrid;

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
            });

        builder.Services.AddMauiBlazorWebView();

        // Configurar HttpClient
        builder.Services.AddHttpClient<AlumnoApiService>(client =>
        {
            client.Timeout = TimeSpan.FromSeconds(30);
            // Agregar headers si es necesario
            client.DefaultRequestHeaders.Add("User-Agent", "MauiHybrid-App");
        });

        // Registrar el servicio de API
        builder.Services.AddScoped<AlumnoApiService>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}