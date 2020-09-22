using Microsoft.EntityFrameworkCore.Sqlite.Migrations.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASOG_OBRAS.Classes
{
    public class Paginator<T>
    {
        private IList<T> DataList;
        private int PageSize;
        private int TotalPages;
        public Paginator(IList<T> list, int size)
        {
            this.DataList = list;
            this.PageSize = size;
            this.TotalPages = (int)Math.Ceiling(this.DataList.Count() / (double)PageSize);
        }

        public IList<T> GetPage(int page)
        {
            return this.DataList.Skip((page - 1) * this.PageSize).Take(page * this.PageSize).ToArray();
        }

        public bool HasNext(int currentPage)
        {
            return this.TotalPages > currentPage;
        }

        public bool HasPrevious(int currentPage)
        {
            return 1 < currentPage;
        }
    }
}
