namespace Active_Blog_Service.ViewModels
{
    public class PaginationBlogViewModel
    {
        public int Currentpage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public List<ShowBlogViewModel> ShowBlogList { get; set; }
    }
}
