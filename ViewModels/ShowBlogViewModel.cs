namespace Active_Blog_Service.ViewModels
{
    public class ShowBlogViewModel
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly CreatedDate { get; set; }
        public string Image { get; set; }
        public string UserId { get; set; }
    }
}
