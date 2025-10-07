using Microsoft.Extensions.Logging;
using MFBauphysikMobilMAUI.CustomRenderer;
using SQLitePCL;

#if ANDROID
using MFBauphysikMobilMAUI.Platforms.Android;
#endif
#if IOS
using MFBauphysikMobilMAUI.Platforms.iOS;
#endif

namespace MFBauphysikMobilMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Batteries_V2.Init(); 

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMauiHandlers(handlers =>
                {
#if ANDROID
                    handlers.AddHandler<MyViewCell, CustomViewCellHandler>();
#endif
/*#if IOS             
                    handlers.AddHandler<MyViewCell, CustomViewCellHandler>();
#endif*/
                });
              /*  .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });*/
            
            /*builder.ConfigureEffects(effects =>
            {
                effects.Add<ReturnKeyEffect, ReturnKeyPlatformEffect>();
            });*/


            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
            });


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
