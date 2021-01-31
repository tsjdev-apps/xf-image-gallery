using System.Collections.Generic;
using System.Threading.Tasks;

namespace XFImageGallery.Interfaces
{
    public interface IImageService
    {
        Task<List<string>> GetImagesAsync(string searchQuery, int page = 0, int perPage = 20);
    }
}
