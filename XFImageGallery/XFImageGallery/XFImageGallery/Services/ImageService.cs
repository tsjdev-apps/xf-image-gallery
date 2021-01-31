using PixabaySharp;
using PixabaySharp.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using XFImageGallery.Interfaces;
using XFImageGallery.Utils;

namespace XFImageGallery.Services
{
    public class ImageService : IImageService
    {
        private readonly PixabaySharpClient _pixabaySharpClient;

        public ImageService()
        {
            _pixabaySharpClient = new PixabaySharpClient(Statics.ApiKey);
        }

        public async Task<List<string>> GetImagesAsync(string searchQuery, int page = 0, int perPage = 20)
        {
            try
            {
                page++;

                var imageQueryBuilder = new ImageQueryBuilder { Query = searchQuery, Page = page, PerPage = perPage };
                var imageResult = await _pixabaySharpClient.QueryImagesAsync(imageQueryBuilder);

                return imageResult.Images.Select(i => i.PreviewURL).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{GetType().Name} | {nameof(GetImagesAsync)} | {ex}");
            }

            return new List<string>();
        }
    }
}
