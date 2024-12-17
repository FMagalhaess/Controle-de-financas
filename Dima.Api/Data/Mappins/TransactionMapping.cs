using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappins;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        builder.Property(x => x.Type)
            .HasColumnType("SMALLINT");
        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("MONEY");
        builder.Property(x => x.CreationDate)
            .IsRequired();
        builder.Property(x => x.PaidOrRecievedDate)
            .IsRequired(false);
        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);
    }
}