using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.ViewModels
{
    public class SearchResult<T> where T: class
    {
        public int totalItems { get; set; }
        public IList<T> items { get; set; }
    }
}
