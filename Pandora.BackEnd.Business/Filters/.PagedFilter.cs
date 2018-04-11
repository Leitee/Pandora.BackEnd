namespace Pandora.BackEnd.Business.Filters
{
    public abstract class PagedFilter
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
