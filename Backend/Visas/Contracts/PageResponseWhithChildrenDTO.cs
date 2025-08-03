namespace Visas.Contracts
{
    public class PageResponseWhithChildrenDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Content { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
        public bool IsActive { get; set; }
        public List<PageResponseWhithChildrenDTO> Children { get; set; } = new();
    }
}
