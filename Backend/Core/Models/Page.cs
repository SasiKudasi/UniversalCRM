using CSharpFunctionalExtensions;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class Page
    {
        public Guid Id { get; set; }
        public string Title { get; private set; } = null!;
        public string Path { get; private set; }
        public string? Content { get; private set; }
        public bool IsActive { get; private set; } = true;
        public bool IsRootPage { get; set; } = false;
        public int OrdinalNuber { get; set; }
        // SEO
        public string? MetaTitle { get; private set; }
        public string? MetaDescription { get; private set; }
        public string? MetaKeywords { get; private set; }

        // Иерархия
        [JsonIgnore]
        public Guid? ParentId { get; private set; }
        public Page? Parent { get; private set; }
        public List<Page> Children { get; private set; } = new();
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; }


        private Page(Guid id, string title,
            string path, string content,
            bool isActive, bool isRoot,
            int ordinalNum, string metaTitle, string metaDiscr, string metaKeywords,
            Guid? parentId = null, Page? parent = null)
        {
            Id = id;
            Title = title;
            Path = path;
            Content = content;
            IsActive = isActive;
            IsRootPage = isRoot;
            OrdinalNuber = ordinalNum;
            MetaTitle = metaTitle;
            MetaDescription = metaDiscr;
            MetaKeywords = metaKeywords;
            ParentId = parentId;
            Parent = parent;
        }

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

        public static Result<Page> Create(Guid id, string title,
            string path, string content,
            bool isActive, bool isRoot,
            int ordinalNum, string metaTitle, string metaDiscr, string metaKeywords,
            Guid? parentId = null, Page? parent = null)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result.Failure<Page>("Title cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(path))
            {
                return Result.Failure<Page>("Path cannot be empty");
            }
            var page = new Page(id, title, 
                path, content, 
                isActive, isRoot, 
                ordinalNum, metaTitle,
                metaDiscr, metaKeywords, 
                parentId, parent);

            return Result.Success<Page>(page);
        }
    }
}
