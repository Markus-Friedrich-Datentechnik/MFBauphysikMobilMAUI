using Microsoft.Extensions.Logging;
using MFBauphysikMobilMAUI.CustomRenderer;
using SQLitePCL;

#if ANDROID
using MFBauphysikMobilMAUI.Platforms.Android;
using AndroidX.Core.View;
using Microsoft.Maui.Handlers;

using AndroidView = Android.Views.View;
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


                });

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
    /*var topInsets = insets.GetInsets(
            WindowInsetsCompat.Type.StatusBars()
            | WindowInsetsCompat.Type.DisplayCutout());

        var bottomInsets = insets.GetInsets(
            WindowInsetsCompat.Type.NavigationBars());
    
     view.SetPadding(
            _paddingLeft,
            _paddingTop + topInsets.Top,
            _paddingRight,
            _paddingBottom + bottomInsets.Bottom);*/
}