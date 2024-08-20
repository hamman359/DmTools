using DmTools.Domain.Entities;
using DmTools.Persistence.Constants;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DmTools.Persistence.Configurations;
internal sealed class ListItemConfiguration : IEntityTypeConfiguration<ListItem>
{
    public void Configure(EntityTypeBuilder<ListItem> builder)
    {
        builder.ToTable(TableNames.ListItems);

        builder.HasKey(x => x.Id);
    }
}
