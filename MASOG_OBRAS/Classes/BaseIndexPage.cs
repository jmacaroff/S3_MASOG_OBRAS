using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASOG_OBRAS.Classes
{
    public abstract class BaseIndexPage<T>: PageModel
    {
        public int CurrentPage;
        private Paginator<T> Paginator { get; set; }

        public void Init(IList<T> list, int page, int size)
        {
            this.CurrentPage = page;
            this.Paginator = new Paginator<T>(list, size);
        }

        public IList<T> LoadPage()
        {
            IList<T> list = this.Paginator.GetPage(this.CurrentPage);
            return list;
        }

        public bool HasNext()
        {
            return this.Paginator.HasNext(CurrentPage);
        }

        public bool HasPrevious()
        {
            return this.Paginator.HasPrevious(CurrentPage);
        }

    }
}
