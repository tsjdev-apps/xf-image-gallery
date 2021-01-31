using System.Collections.Generic;

namespace XFImageGallery.ExtensionMethods
{
    public static class EnumerableExtensions
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> collection)
        {
            foreach (var item in collection)
                list.Add(item);
        }
    }
}
