using Core.Models;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mappers
{
    internal static class PageMapper
    {
        public static Page ToDomain(this PageEntity entity, HashSet<int>? visited = null)
        {
            visited ??= new HashSet<int>();

            if (visited.Contains(entity.Id))
            {
                // Уже обработан — возвращаем null или заглушку, чтобы избежать зацикливания
                return null!;
            }
            visited.Add(entity.Id);

            var page = Page.Create(
                id: entity.Id,
               title: entity.Title,
               slug: entity.Slug,
               content: entity.HtmlContent,
               metaTitle: entity.MetaTitle,
               metaDescription: entity.MetaDescription,
               metaKeywords: entity.MetaKeywords

             );
            page.SetCreatedAt(entity.CreatedAt);
            page.SetUpdatedAt(entity.UpdatedAt);
            page.SetActive(entity.IsActive);

            //if (entity.Parent != null)
            //{
            //    var parentDomain = entity.Parent.ToDomain();
            //    page.SetParent(parentDomain);
            //}

            foreach (var childEntity in entity.Children)
            {
                var childDomain = childEntity.ToDomain(visited);
                if (childDomain != null)
                    page.AddChild(childDomain);
            }

            return page;
        }


        public static PageEntity ToDAL(this Page page)
        {
            if (page == null) return null!;

            var entity = new DAL.Entity.PageEntity
            {
                Id = page.Id,
                Title = page.Title,
                Slug = page.Slug,
                HtmlContent = page.Content,
                MetaTitle = page.MetaTitle,
                MetaDescription = page.MetaDescription,
                IsActive = page.IsActive,
                CreatedAt = page.CreatedAt,
                UpdatedAt = page.UpdatedAt,
                ParentId = page.ParentId
            };

            // Родитель
            entity.Parent = page.Parent?.ToDAL();

            // Дети
            entity.Children = page.Children.Select(c => c.ToDAL()).ToList();

            return entity;
        }
    }
}
