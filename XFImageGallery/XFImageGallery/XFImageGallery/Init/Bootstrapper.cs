using CommonServiceLocator;
using Unity;
using Unity.Lifetime;
using Unity.ServiceLocation;
using XFImageGallery.Interfaces;
using XFImageGallery.Services;
using XFImageGallery.ViewModels;

namespace XFImageGallery.Init
{
    public static class Bootstrapper
    {
        public static void RegisterDependencies()
        {
            var container = new UnityContainer();

            // service
            container.RegisterType<IImageService, ImageService>(new ContainerControlledLifetimeManager());

            // viewmodel
            container.RegisterType<ImageViewModel>(new ContainerControlledLifetimeManager());

            var locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}
