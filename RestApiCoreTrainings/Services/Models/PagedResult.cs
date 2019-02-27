﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Models
{
    public class PagedList<T> : List<T>
    {
        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public PagedList(List<T> items, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(items.Count / (double)pageSize);
        
            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public PagedList<T> GetPage(List<T> source, int pageIndex, int pageSize)
        {
            var itemsToSkip = (pageIndex - 1) * pageSize;

            var items = source.Skip(itemsToSkip).Take(pageSize).ToList();

            return new PagedList<T>(items, pageIndex, pageSize);          
        }
    }
}