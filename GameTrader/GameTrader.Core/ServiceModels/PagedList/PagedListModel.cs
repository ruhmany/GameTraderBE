using X.PagedList;

namespace GameTrader.Core.ServiceModels.PagedList
{
    public class PagedListModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PagedListMetaData MetaData { get; set; }
        public PagedListModel(IEnumerable<T> items, PagedListMetaData metaData) 
            => (Items, MetaData) = (items, metaData);
        public PagedListModel()
        {
            
        }
    }
}
