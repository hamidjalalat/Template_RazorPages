namespace Persistence.Features.Idenity.Configurations;

internal sealed class BaseTableConfiguration : object, Microsoft
	.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Features.Common.BaseTable>
{
	public BaseTableConfiguration() : base()
	{
	}

	public void Configure(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Features.Common.BaseTable> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		////using Microsoft.EntityFrameworkCore;
		//builder.ToTable(nameof(DatabaseContext.BaseTables),
		//	schema: Domain.Features.Common.Schema.Name);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasIndex(current => new { current.Code })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasMany(current => current.LocalizedBaseTables)
			.WithOne(other => other.BaseTable)
			.IsRequired(required: true)
			.HasForeignKey(other => other.BaseTableId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.BaseTableItems)
			.WithOne(other => other.BaseTable)
			.IsRequired(required: true)
			.HasForeignKey(other => other.BaseTableId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
