﻿using System.Linq;

namespace Pandora.BackEnd.Data.Helpers
{
    public class PagedResult<TEntity> where TEntity : class
    {
        public int CurrentPage { get; set; }

        public int RowsPerPage { get; set; }

        public int CollectionLength { get; set; }

        public double TotalPages { get; set; }

        public IQueryable<TEntity> Collection { get; set; }
    }
}
