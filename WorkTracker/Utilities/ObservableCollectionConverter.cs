using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WorkTracker.Utilities
{
    public static class ObservableCollectionConverter
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this ICollection<T> list)
        {
            ObservableCollection<T> collection = new();
            foreach (var item in list)
                collection.Add(item);
            return collection;
        }
    }
}
