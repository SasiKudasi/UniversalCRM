using Core.Models;
using CSharpFunctionalExtensions;

namespace Visas.Contracts.Mapper
{
    public static class PageMapper
    {
        public static Result<Page> ToDomain(this PageRequestDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            var pageId = Guid.NewGuid();
            var page = Page.Create(
                id: pageId,
                title: dto.Title,
                path: dto.Path,
                content: dto.Content,
                isActive: dto.IsActive,
                isRoot: false,
                ordinalNum: dto.OrdinalNum,
                metaTitle: dto.MetaTitle,
                metaDiscr: dto.MetaDescription,
                metaKeywords: dto.MetaKeywords
            );
            return page;
        }


        public static PageResponseWhithChildrenDTO ToResponseWhithChildren(this Page page)
        {
            return new PageResponseWhithChildrenDTO
            {
                Id = page.Id,
                Title = page.Title,
                Path = page.Path,
                Content = page.Content,
                MetaTitle = page.MetaTitle,
                MetaDescription = page.MetaDescription,
                MetaKeywords = page.MetaKeywords,
                IsActive = page.IsActive,
                IsRoot = page.IsRootPage,
                OrdinalNum = page.OrdinalNuber,
                Children = page.Children.Select(c => c.ToResponseWhithChildren()).ToList()
            };
        }

        public static SoloPageResponceDTO ToResponse(this Page page)
        {
            return new SoloPageResponceDTO
            {
                Id = page.Id,
                Title = page.Title,
                Path = page.Path,
                Content = page.Content,
                MetaTitle = page.MetaTitle,
                MetaDescription = page.MetaDescription,
                MetaKeywords = page.MetaKeywords,
                IsActive = page.IsActive
            };
        }

    }
}
