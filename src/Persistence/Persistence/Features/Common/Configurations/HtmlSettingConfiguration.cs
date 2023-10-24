namespace Persistence.Features.Common.Configurations;

internal sealed class HtmlSettingConfiguration : object, Microsoft
	.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Features.Common.HtmlSetting>
{
	public HtmlSettingConfiguration() : base()
	{
	}

	public void Configure(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Features.Common.HtmlSetting> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		// دستور ذیل بسیار مهم می‌باشد
		//
		// دستور ذیل باعث می‌شود که در این جدول، به
		// اشتباه، بیش از یک رکورد، در بانک اطلاعاتی ایجاد نگردد
		builder
			.HasIndex(current => new { current.Id })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
