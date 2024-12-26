using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
    public class ProductSpecParams
    {
      public  string? Sort { set; get; }
        public int? BrandId { set; get; }
        public int? CategoryId { set; get; }
        private int pagesize=5;
        public int PageSize
            { get { return pagesize; }
            set { pagesize = value > 10 ? 10 : value; } }
        public int PageIndex { set; get; } = 1;

        private string? search;
               public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }


    }
}
