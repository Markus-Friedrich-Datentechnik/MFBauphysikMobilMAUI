using MFBauphysikMobilMAUI.Data;
using MFBauphysikMobilMAUI.Helpers;

namespace MFBauphysikMobilMAUI
{
    public partial class App : Application
    {
        private static Database database;
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new
                        Database(Path.Combine(Environment.GetFolderPath(Environment.
                        SpecialFolder.LocalApplicationData), "Bauphysik"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }
        protected override void OnStart()
        {
            OnResume();
        }

        //laufen im Background
        protected override void OnSleep()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged -= App_Theme_Changed;
        }

        protected override void OnResume()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged += App_Theme_Changed;
        }
        private void App_Theme_Changed(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TheTheme.SetTheme();
            });
        }
    }
}
