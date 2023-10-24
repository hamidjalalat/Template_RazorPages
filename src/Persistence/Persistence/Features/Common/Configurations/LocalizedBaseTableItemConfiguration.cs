namespace Persistence.Features.Idenity.Configurations;

internal sealed class LocalizedBaseTableItemConfiguration : object, Microsoft
	.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Features.Common.LocalizedBaseTableItem>
{
	public LocalizedBaseTableItemConfiguration() : base()
	{
	}

	public void Configure(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Features.Common.LocalizedBaseTableItem> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasIndex(current => new { current.CultureId, current.BaseTableItemId })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
