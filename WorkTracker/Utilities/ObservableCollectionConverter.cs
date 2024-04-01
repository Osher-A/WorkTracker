using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WorkTracker.WPF.Utilities
{
    public static class ObservableCollectionConverter
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this ICollection<T> list)
        {
            ObservableCollection<T> collection = [.. list];
            return collection;
        }
    }
}
