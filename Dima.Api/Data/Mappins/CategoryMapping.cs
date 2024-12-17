using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappins;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Title)
            .IsRequired()
            .HasColumnType("NVARCHAR(MAX)")
            .HasMaxLength(80);
        builder.Property(c => c.Description)
            .IsRequired(false)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(255);
        builder.Property(c => c.UserId)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);
    }
}