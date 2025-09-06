namespace Visas.Contracts
{
    public class SoloPageResponceDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Path { get; set; } = null!;
        public string? Content { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
        public bool IsActive { get; set; }
    }
}
