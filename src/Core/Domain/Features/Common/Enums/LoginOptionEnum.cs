namespace Domain.Features.Common.Enums;

public enum LoginOptionEnum : int
{
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ThroughBoth))]
	Both = 0,

	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ThroughUsername))]
	Username = 1,

	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ThroughEmailAddress))]
	EmailAddress = 2,
}
