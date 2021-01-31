using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using XFImageGallery.ExtensionMethods;
using XFImageGallery.Interfaces;
using XFImageGallery.Utils;

namespace XFImageGallery.ViewModels
{
    public class ImageViewModel : BaseViewModel
    {
        private readonly IImageService _imageService;

        private int _currentPage = -1;


        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set { _searchQuery = value; OnPropertyChanged(); LoadImagesAsyncCommand.RaiseCanExecuteChange(); }
        }

        private ObservableCollection<string> _images = new ObservableCollection<string>();
        public ObservableCollection<string> Images
        {
            get => _images;
            set { _images = value; OnPropertyChanged(); }
        }

        private bool _isIncrementalLoading;
        public bool IsIncrementalLoading
        {
            get => _isIncrementalLoading;
            set { _isIncrementalLoading = value; OnPropertyChanged(); }
        }


        private AsyncRelayCommand _loadImagesAsyncCommand;
        public AsyncRelayCommand LoadImagesAsyncCommand
                => _loadImagesAsyncCommand ?? (_loadImagesAsyncCommand = new AsyncRelayCommand(LoadImagesAsync, CanLoadImages));

        private AsyncRelayCommand _loadMoreImagesAsyncCommand;
        public AsyncRelayCommand LoadMoreImagesAsyncCommand
                => _loadMoreImagesAsyncCommand ?? (_loadMoreImagesAsyncCommand = new AsyncRelayCommand(LoadMoreImagesAsync, CanLoadImages));

        private bool CanLoadImages()
                => !string.IsNullOrEmpty(SearchQuery);


        public ImageViewModel(IImageService imageService)
        {
            _imageService = imageService;
        }


        private async Task LoadImagesAsync()
        {
            try
            {
                IsRefreshing = true;

                _currentPage = 0;

                Images.Clear();

                var images = await _imageService.GetImagesAsync(SearchQuery, _currentPage);

                Images.AddRange(images);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{GetType().Name} | {nameof(LoadImagesAsync)} | {ex}");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private async Task LoadMoreImagesAsync()
        {
            if (IsIncrementalLoading)
                return;

            try
            {
                IsIncrementalLoading = true;

                _currentPage++;

                var images = await _imageService.GetImagesAsync(SearchQuery, _currentPage, 20);

                if (!images.Any())
                    return;

                Images.AddRange(images);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{GetType().Name} | {nameof(LoadMoreImagesAsync)} | {ex}");
            }
            finally
            {
                IsIncrementalLoading = false;
            }
        }
    }
}
