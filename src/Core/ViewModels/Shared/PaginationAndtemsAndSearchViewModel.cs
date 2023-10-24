namespace ViewModels.Shared;

public class PaginationAndtemsAndSearchViewModel
	<TItem, TSearch> : PaginationAndItemsViewModel<TItem> where TSearch : new()
{
	public PaginationAndtemsAndSearchViewModel() : base()
	{
		Search = new();
	}

	public TSearch Search { get; set; }
}
