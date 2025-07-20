namespace Visas.Contracts
{
    public class PageRequestDTO
    {
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Content { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
        public bool IsActive { get; set; } = true;
        public int? ParentId { get; set; }
    }
}
