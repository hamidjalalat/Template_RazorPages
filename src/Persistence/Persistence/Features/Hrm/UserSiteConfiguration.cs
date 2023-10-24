namespace Persistence.Features.Hrm.Configurations;

internal sealed class UserSiteConfiguration : object, Microsoft
	.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Features.Hrm.UserSite>
{
	public UserSiteConfiguration() : base()
	{
	}

	public void Configure(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Features.Hrm.UserSite> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.Property(current => current.SiteUrl)
			.IsUnicode(unicode: false)
			;

		builder
			.HasIndex(current => new { current.UserId, current.SiteUrl })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
