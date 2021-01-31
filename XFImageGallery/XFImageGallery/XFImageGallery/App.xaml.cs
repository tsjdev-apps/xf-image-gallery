using Xamarin.Forms;
using XFImageGallery.Init;

namespace XFImageGallery
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Bootstrapper.RegisterDependencies();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
