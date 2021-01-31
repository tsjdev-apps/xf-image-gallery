using Xamarin.Forms;
using XFImageGallery.Init;
using XFImageGallery.Views;

namespace XFImageGallery
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Bootstrapper.RegisterDependencies();

            MainPage = new NavigationPage(new ImagesPage());
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
