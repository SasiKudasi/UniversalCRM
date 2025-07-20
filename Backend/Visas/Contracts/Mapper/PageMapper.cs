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

            //if (dto.ParentId.HasValue)
            //{
            //    // Подразумеваем, что родитель уже есть в БД и его ID мы передаём
            //    page.SetParent(new Page { Id = dto.ParentId.Value });
            //}

            return page;
        }
    }
}
