using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ECBIM.API.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Transformer un enumérable en ObservableCollection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">Collection à transformer</param>
        /// <returns>ObservableCollection de la collection</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            return new ObservableCollection<T>(collection);
        }

        /// <summary>
        /// Transposer une liste de listes.
        /// </summary>
        /// <example>[[a, b, c], [1, 2, 3]] ==> [[a, 1], [b, 2], [c, 3]]</example>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="lists">Liste à transposée</param>
        /// <returns>Liste transposée</returns>
        public static List<List<T>> Transpose<T>(this List<List<T>> lists)
        {
            var longest = lists.Any() ? lists.Max(l => l.Count) : 0;
            List<List<T>> transposedList = new List<List<T>>(longest);
            for (int i = 0; i < longest; i++)
            {
                transposedList.Add(new List<T>(lists.Count));
            }

            for (int j = 0; j < lists.Count; j++)
            {
                for (int i = 0; i < longest; i++)
                {
                    transposedList[i].Add(lists[j].Count > i ? lists[j][i] : default);
                }
            }

            return transposedList;
        }

        public static List<T> OrEmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source.ToList() ?? new List<T>();
        }
    }
}
