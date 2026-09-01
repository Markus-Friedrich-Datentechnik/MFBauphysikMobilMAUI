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
                    NavigationViewHandler.Mapper.AppendToMapping(
                    "AndroidWindowInsets",
                    (handler, view) =>
                    {
                        var platformView  = handler.PlatformView;
                        ViewCompat.SetOnApplyWindowInsetsListener(
                            platformView,
                            new NavigationPageInsetsListener());
                        ViewCompat.RequestApplyInsets(platformView);
                    });

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
#if ANDROID

    public sealed class NavigationPageInsetsListener
    : Java.Lang.Object, IOnApplyWindowInsetsListener
{
    public WindowInsetsCompat OnApplyWindowInsets(
        AndroidView view,
        WindowInsetsCompat insets)
    {
        var topInsets = insets.GetInsets(
            WindowInsetsCompat.Type.StatusBars()
            | WindowInsetsCompat.Type.DisplayCutout());

        view.SetPadding(
            view.PaddingLeft,
            topInsets.Top,
            view.PaddingRight,
            view.PaddingBottom);

        return insets;
    }
}

#endif

}
