namespace ViewModels.Shared;

public class PaginationAndItemsViewModel<TItem> : object
{
	public PaginationAndItemsViewModel() : base()
	{
		Sorting = new();
		Pagination = new();

		Items = new System
			.Collections.Generic.List<TItem>();
	}

	public SortingViewModel Sorting { get; set; }

	public PaginationViewModel Pagination { get; set; }

	public System.Collections.Generic.IList<TItem> Items { get; set; }
}
