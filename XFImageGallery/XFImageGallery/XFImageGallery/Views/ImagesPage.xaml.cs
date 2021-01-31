using CommonServiceLocator;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFImageGallery.ViewModels;

namespace XFImageGallery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagesPage : ContentPage
    {
        public ImagesPage()
        {
            InitializeComponent();

            BindingContext = ServiceLocator.Current.GetInstance<ImageViewModel>();

            SetLayout();
            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            if (Device.Idiom == TargetIdiom.Tablet || Device.Idiom == TargetIdiom.Desktop || Device.Idiom == TargetIdiom.TV)
            {
                var itemsPerRow = (int)(Width / 212);
                ((GridItemsLayout)ImagesCollectionView.ItemsLayout).Span = itemsPerRow;
            }
        }

        private void SetLayout()
        {
            switch (Device.Idiom)
            {
                case TargetIdiom.Unsupported:
                case TargetIdiom.Watch:
                    ImagesCollectionView.ItemsLayout = new GridItemsLayout(ItemsLayoutOrientation.Vertical) { Span = 1, HorizontalItemSpacing = 12, VerticalItemSpacing = 12 };
                    break;
                case TargetIdiom.Phone:
                    ImagesCollectionView.ItemsLayout = new GridItemsLayout(ItemsLayoutOrientation.Vertical) { Span = 2, HorizontalItemSpacing = 12, VerticalItemSpacing = 12 };
                    break;
                case TargetIdiom.Tablet:
                case TargetIdiom.Desktop:
                case TargetIdiom.TV:
                    ImagesCollectionView.ItemsLayout = new GridItemsLayout(ItemsLayoutOrientation.Vertical) { Span = 4, HorizontalItemSpacing = 12, VerticalItemSpacing = 12 };
                    break;
            }
        }

        // Workaround for UWP bug 
        // => https://github.com/xamarin/Xamarin.Forms/issues/9013
        private void ImagesCollectionViewOnScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (Device.RuntimePlatform != Device.UWP)
                return;

            if (sender is CollectionView collectionView && collectionView is IElementController elementController)
            {
                var count = elementController.LogicalChildren.Count;
                if (e.LastVisibleItemIndex + 1 - count
                    + collectionView.RemainingItemsThreshold >= 0)
                {
                    if (collectionView.RemainingItemsThresholdReachedCommand.CanExecute(null))
                        collectionView.RemainingItemsThresholdReachedCommand.Execute(null);
                }
            }
        }
    }
}