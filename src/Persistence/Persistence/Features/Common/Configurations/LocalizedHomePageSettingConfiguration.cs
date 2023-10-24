namespace Persistence.Features.Common.Configurations;

internal sealed class LocalizedHomePageSettingConfiguration : object, Microsoft
	.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Features.Common.LocalizedHomePageSetting>
{
	public LocalizedHomePageSettingConfiguration() : base()
	{
	}

	public void Configure(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Features.Common.LocalizedHomePageSetting> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		// دستور ذیل بسیار مهم می‌باشد
		//
		// دستور ذیل باعث می‌شود که در این جدول، به
		// ازای هر زبان، به اشتباه، بیش از یک رکورد، در بانک اطلاعاتی ایجاد نگردد
		builder
			.HasIndex(current => new { current.CultureId })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
