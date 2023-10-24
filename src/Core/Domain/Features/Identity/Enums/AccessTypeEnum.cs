namespace Domain.Features.Identity.Enums;

public enum AccessTypeEnum : int
{
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.AccessTypePublic))]
	Public = 0,

	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.AccessTypeRegistered))]
	Registered = 1,

	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.AccessTypeSpecial))]
	Special = 2,
}
