namespace ViewModels.Shared;

public class SortingViewModel : object
{
	public SortingViewModel() : base()
	{
	}

	public string? Expression { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? SelectList { get; set; }
}
