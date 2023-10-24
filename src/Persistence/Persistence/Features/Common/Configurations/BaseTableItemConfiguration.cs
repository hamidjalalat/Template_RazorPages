namespace Persistence.Features.Idenity.Configurations;

internal sealed class BaseTableItemConfiguration : object, Microsoft
	.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Features.Common.BaseTableItem>
{
	public BaseTableItemConfiguration() : base()
	{
	}

	public void Configure(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Features.Common.BaseTableItem> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		////using Microsoft.EntityFrameworkCore;
		//builder.ToTable(nameof(DatabaseContext.BaseTableItems),
		//	schema: Domain.Features.Common.Schema.Name);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.Property(current => current.KeyName)
			.IsUnicode(unicode: false)
			;

		builder
			.HasIndex(current => new { current.BaseTableId, current.KeyName })
			.IsUnique(unique: true)
			;
		// **************************************************

		// **************************************************
		builder
			.HasIndex(current => new { current.BaseTableId, current.Code })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasMany(current => current.LocalizedBaseTableItems)
			.WithOne(other => other.BaseTableItem)
			.IsRequired(required: true)
			.HasForeignKey(other => other.BaseTableItemId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasMany(current => current.Users_Role)
			.WithOne(other => other.Role)
			// Note: Is Not Required!
			.IsRequired(required: false)
			.HasForeignKey(other => other.RoleId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.Users_Gender)
			.WithOne(other => other.Gender)
			// Note: Is Not Required!
			.IsRequired(required: false)
			.HasForeignKey(other => other.GenderId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.Users_Religion)
			.WithOne(other => other.Religion)
			// Note: Is Not Required!
			.IsRequired(required: false)
			.HasForeignKey(other => other.ReligionId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.Users_MaritalStatus)
			.WithOne(other => other.MaritalStatus)
			// Note: Is Not Required!
			.IsRequired(required: false)
			.HasForeignKey(other => other.MaritalStatusId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.Users_LastEducationDegree)
			.WithOne(other => other.LastEducationDegree)
			// Note: Is Not Required!
			.IsRequired(required: false)
			.HasForeignKey(other => other.LastEducationDegreeId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.Users_MilitaryServiceStatus)
			.WithOne(other => other.MilitaryServiceStatus)
			// Note: Is Not Required!
			.IsRequired(required: false)
			.HasForeignKey(other => other.MilitaryServiceStatusId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
