namespace Domain.Features.Identity.Enums;

public enum AuthenticationTypeEnum : int
{
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.AuthenticationTypeInternal))]
	Internal = 0,

	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.AuthenticationTypeGoogle))]
	Google = 1,
}
