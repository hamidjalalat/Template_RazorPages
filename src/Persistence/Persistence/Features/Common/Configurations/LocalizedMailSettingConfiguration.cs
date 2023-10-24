namespace Persistence.Features.Common.Configurations;

internal sealed class LocalizedMailSettingConfiguration : object, Microsoft
	.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Features.Common.LocalizedMailSetting>
{
	public LocalizedMailSettingConfiguration() : base()
	{
	}

	public void Configure(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Features.Common.LocalizedMailSetting> builder)
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
