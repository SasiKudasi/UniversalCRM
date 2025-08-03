using Core.Models;

namespace Visas.Contracts.Mapper
{
    public static class PageMapper
    {
        public static Page ToDomain(this PageRequestDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var page = Page.Create(
                title: dto.Title,
                slug: dto.Slug,
                content: dto.Content,
                metaTitle: dto.MetaTitle,
                metaDescription: dto.MetaDescription,
                metaKeywords: dto.MetaKeywords
            );

            page.SetActive(dto.IsActive);
            return page;
        }


        public static PageResponseWhithChildrenDTO ToResponseWhithChildren(this Page page)
        {
            return new PageResponseWhithChildrenDTO
            {
                Id = page.Id,
                Title = page.Title,
                Slug = page.Slug,
                Content = page.Content,
                MetaTitle = page.MetaTitle,
                MetaDescription = page.MetaDescription,
                MetaKeywords = page.MetaKeywords,
                IsActive = page.IsActive,
                Children = page.Children.Select(c => c.ToResponseWhithChildren()).ToList()
            };
        }

        public static SoloPageResponceDTO ToResponse(this Page page)
        {
            return new SoloPageResponceDTO
            {
                Id = page.Id,
                Title = page.Title,
                Slug = page.Slug,
                Content = page.Content,
                MetaTitle = page.MetaTitle,
                MetaDescription = page.MetaDescription,
                MetaKeywords = page.MetaKeywords,
                IsActive = page.IsActive
            };
        }

    }
}
