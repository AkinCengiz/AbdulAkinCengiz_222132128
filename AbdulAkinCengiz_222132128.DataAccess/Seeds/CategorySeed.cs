using AbdulAkinCengiz_222132128.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.DataAccess.Seeds;
internal class CategorySeed : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            new Category()
            {
                Id = 1,
                Name = "Çorba",
                CreateAt = new DateTime(2025, 11, 8),
                IsDeleted = false,
                IsActive = true
            },
            new Category()
            {
                Id = 2,
                Name = "İçecek",
                CreateAt = new DateTime(2025, 11, 8),
                IsDeleted = false,
                IsActive = true
            },
            new Category()
            {
                Id = 3,
                Name = "Ana Yemek",
                CreateAt = new DateTime(2025, 11, 8),
                IsDeleted = false,
                IsActive = true
            },
            new Category()
            {
                Id = 4,
                Name = "Tatlı",
                CreateAt = new DateTime(2025, 11, 8),
                IsDeleted = false,
                IsActive = true
            },
            new Category()
            {
                Id = 5,
                Name = "Salata",
                CreateAt = new DateTime(2025, 11, 8),
                IsDeleted = false,
                IsActive = true
            },
            new Category()
            {
                Id = 6,
                Name = "Pide",
                CreateAt = new DateTime(2025, 11, 8),
                IsDeleted = false,
                IsActive = true
            },
            new Category()
            {
                Id = 7,
                Name = "Izgara",
                CreateAt = new DateTime(2025, 11, 8),
                IsDeleted = false,
                IsActive = true
            },
            new Category()
            {
                Id = 8,
                Name = "Ara Sıcak",
                CreateAt = new DateTime(2025, 11, 8),
                IsDeleted = false,
                IsActive = true
            }
            );
    }
}
