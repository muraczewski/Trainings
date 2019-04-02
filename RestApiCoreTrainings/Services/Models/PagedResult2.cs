using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Models
{
    public class PagedList2<T> : List<T>
    {
        public ICollection<T> Items { get; set; }

        public int? PageIndex { get; set; }

        public int TotalPages { get; set; }

        public int TotalItems { get; set; }

        public PagedList2(ICollection<T> items, int count, int? pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalItems = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }


        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static PagedList2<T> GetPage(ICollection<T> items, int? pageIndex, int pageSize)
        {
            if (pageIndex == null)
            {
                return new PagedList2<T>(items, items.Count, 1, items.Count);
            }

            var itemsToSkip = (pageIndex.Value - 1) * pageSize;
            var count = items.Count;

            var result = items.Skip(itemsToSkip).Take(pageSize).ToList();

            return new PagedList2<T>(result, count, pageIndex, pageSize);          
        }
    }
}
