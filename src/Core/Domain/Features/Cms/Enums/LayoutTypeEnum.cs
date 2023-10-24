namespace Domain.Features.Cms.Enums;

public enum LayoutTypeEnum : int
{
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.LayoutDefault))]
	Default = 0,

	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.LayoutEmpty))]
	Empty = 1,

	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.LayoutCustom))]
	Custom = 2,
}
