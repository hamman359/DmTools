using DmTools.Domain.Entities;
using DmTools.Persistence.Constants;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DmTools.Persistence.Configurations;

internal sealed class ItemListConfiguration : IEntityTypeConfiguration<ItemList>
{
    public void Configure(EntityTypeBuilder<ItemList> builder)
    {
        builder.ToTable(TableNames.ItemLists);

        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(x => x.ItemListId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
