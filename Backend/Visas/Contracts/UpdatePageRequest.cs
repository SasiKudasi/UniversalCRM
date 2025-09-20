namespace Visas.Contracts
{
    public class UpdatePageRequest
    {
        public string? Title { get; set; }
        public string? Path { get; set; }
        public int OrdinalNum { get; set; }
        public string? Content { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
        public bool IsActive { get; set; }
    }
}
