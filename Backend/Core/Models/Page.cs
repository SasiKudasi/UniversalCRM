using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class Page
    {
        public int Id { get; private set; }

        public string Title { get; private set; } = null!;

        // URL-часть (например, "usa", "faq", "contacts")
        public string Slug { get; private set; } = null!;

        // HTML или rich-текст
        public string? Content { get; private set; }

        // SEO
        public string? MetaTitle { get; private set; }
        public string? MetaDescription { get; private set; }
        public string? MetaKeywords { get; private set; }

        public bool IsActive { get; private set; } = true;

        // Иерархия
        [JsonIgnore]
        public int? ParentId { get; private set; }
        public Page? Parent { get; private set; }
        public List<Page> Children { get; private set; } = new();
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; }


        public void UpdateTimestamps()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
            UpdateTimestamps();
        }

        public void SetParent(Page? parent)
        {
            Parent = parent;
            ParentId = parent?.Id;
            UpdateTimestamps();
        }


        public void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
            // UpdateTimestamps();
        }

        public void SetUpdatedAt(DateTime updatedAt)
        {
            UpdatedAt = updatedAt;
        }

        public void AddChild(Page child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            child.SetParent(this);
            Children.Add(child);
            UpdateTimestamps();
        }

        public static Page Create(string title, string slug, string? content = null,
            string? metaTitle = null, string? metaDescription = null, string? metaKeywords = null
            )
        {


            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty", nameof(title));
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentException("Slug cannot be empty", nameof(slug));
            if (slug.Length > 200)
                throw new ArgumentException("Slug cannot exceed 200 characters", nameof(slug));


            return new Page
            {
                Title = title,
                Slug = slug,
                Content = content,
                MetaTitle = metaTitle,
                MetaDescription = metaDescription,
                MetaKeywords = metaKeywords
            };
        }


        public static Page Create(int id, string title, string slug, string? content = null,
    string? metaTitle = null, string? metaDescription = null, string? metaKeywords = null
    )
        {


            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty", nameof(title));
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentException("Slug cannot be empty", nameof(slug));
            if (slug.Length > 200)
                throw new ArgumentException("Slug cannot exceed 200 characters", nameof(slug));


            return new Page
            {
                Id = id,
                Title = title,
                Slug = slug,
                Content = content,
                MetaTitle = metaTitle,
                MetaDescription = metaDescription,
                MetaKeywords = metaKeywords
            };
        }

    }
}
