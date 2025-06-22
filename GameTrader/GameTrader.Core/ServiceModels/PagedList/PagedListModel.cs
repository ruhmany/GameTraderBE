using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace GameTrader.Core.ServiceModels.PagedList
{
    public class PagedListModel<T>
    {
        public IEnumerable<T> Item { get; set; }
        public PagedListMetaData MetaData { get; set; }
        public PagedListModel(IEnumerable<T> items, PagedListMetaData metaData) 
            => (Item, MetaData) = (items, metaData);
        public PagedListModel()
        {
            
        }
    }
}
