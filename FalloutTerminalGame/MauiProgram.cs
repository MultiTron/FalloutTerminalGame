﻿using FalloutTerminalGame.Resources.Utils;
using FTG.Data;
using FTG.Services.Implementations;
using FTG.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;

namespace FalloutTerminalGame
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
                    fonts.AddFont("PixeloidMono-d94EV.ttf", "PixeloidMono");
                    fonts.AddFont("PixeloidSans-mLxMm.ttf", "PixeloidSans");
                    fonts.AddFont("PixeloidSansBold-PKnYd.ttf", "PixeloidSansBold");
                });
            builder.Services.AddTransient<App>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<WellcomePage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<MainPage>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IWordService, WordService>();

            builder.Services.AddDbContext<GameDbContext>();
            builder.AddAudio();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            ServiceHelper.Initialize(app.Services);

            return app;
        }
    }
}
