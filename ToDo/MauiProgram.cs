using Microsoft.Extensions.Logging;
using ToDo.Database;
using ToDo.Models;
using ToDo.ViewModels;

namespace ToDo;

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

        builder.Services.AddSingleton<IDatabase<Todo>, SQLiteDatabase>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<EditPageViewModel>();
        builder.Services.AddTransient<EditTodoPage>();
        builder.Services.AddTransient<AddPageViewModel>();
        builder.Services.AddTransient<AddTodoPage>();
        builder.Services.AddSingleton<TestPage>();
        builder.Services.AddSingleton<AnotherTest>();

        return builder.Build();
    }
}