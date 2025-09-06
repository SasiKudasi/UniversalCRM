using Core.Models;
using CSharpFunctionalExtensions;
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
        public static Result<Page> ToDomain(this PageEntity entity, HashSet<Guid>? visited = null)
        {
            visited ??= new HashSet<Guid>();

            if (visited.Contains(entity.Id))
            {
                // Уже обработан — возвращаем null или заглушку, чтобы избежать зацикливания
                return null!;
            }
            visited.Add(entity.Id);

            var pageResult = Page.Create(
                id: entity.Id,
               title: entity.Title,
               path: entity.Path,
               content: entity.HtmlContent,
               metaTitle: entity.MetaTitle,
               metaDiscr: entity.MetaDescription,
               metaKeywords: entity.MetaKeywords,
               isActive: entity.IsActive,
               isRoot: entity.IsRootPage,
               ordinalNum: entity.OrdinalNuber

             );
            if (pageResult.IsFailure)
            {
                return Result.Failure<Page>(pageResult.Error);

            }
            //if (entity.Parent != null)
            //{
            //    var parentDomain = entity.Parent.ToDomain();
            //    page.SetParent(parentDomain);
            //}
            var page = pageResult.Value;
            foreach (var childEntity in entity.Children)
            {
                var childDomain = childEntity.ToDomain(visited);
                if (childDomain.IsSuccess)
                {
                    page.AddChild(childDomain.Value);
                }

                //if (childDomain != null)
                //    page.AddChild(childDomain);
            }

            return Result.Success(page);
        }


        public static PageEntity ToDAL(this Page page)
        {
            if (page == null) return null!;

            var entity = new PageEntity
            {
                Id = page.Id,
                Title = page.Title,
                Path = page.Path,
                HtmlContent = page.Content,
                MetaTitle = page.MetaTitle,
                MetaDescription = page.MetaDescription,
                IsActive = page.IsActive,
                IsRootPage = page.IsRootPage,
                OrdinalNuber = page.OrdinalNuber,
                CreatedAt = page.CreatedAt,
                UpdatedAt = page.UpdatedAt,
                ParentId = page.ParentId,
            };

            // Родитель
            entity.Parent = page.Parent?.ToDAL();

            // Дети
            entity.Children = page.Children.Select(c => c.ToDAL()).ToList();

            return entity;
        }
    }
}
