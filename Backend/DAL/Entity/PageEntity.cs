using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace DAL.Entity
{
    public class PageEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = null!;

        [Required, MaxLength(200)]
        public string Slug { get; set; } = null!;

        public string? HtmlContent { get; set; }

        [MaxLength(255)]
        public string? MetaTitle { get; set; }

        [MaxLength(500)]
        public string? MetaDescription { get; set; }
        [MaxLength(500)]
        public string? MetaKeywords { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Вложенность
        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public PageEntity? Parent { get; set; }

        public ICollection<PageEntity> Children { get; set; } = new List<PageEntity>();

    }
}
