﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Models
{
    public class PagedList<T> : List<T>
    {
        public ICollection<T> Items { get; set; }

        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public int TotalItems { get; set; }

        public PagedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalItems = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }


        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static PagedList<T> GetPage(ICollection<T> items, int pageIndex, int pageSize)
        {
            var itemsToSkip = (pageIndex - 1) * pageSize;
            var count = items.Count;

            var result = items.Skip(itemsToSkip).Take(pageSize).ToList();

            return new PagedList<T>(result, count, pageIndex, pageSize);          
        }
    }
}
