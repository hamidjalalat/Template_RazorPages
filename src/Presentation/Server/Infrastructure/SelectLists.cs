using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class SelectLists : object
{
	static SelectLists()
	{
	}

	#region GetLayoutsAsync()
	public static async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.Rendering.SelectList> GetLayoutsAsync
		(Persistence.DatabaseContext databaseContext, object? selectedValue = null)
	{
		var list =
			await
			databaseContext.Layouts
			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Title)
			.Select(current => new ViewModels.Shared.IdNameViewModel<System.Guid?>
			{
				Id = current.Id,
				KeyName = current.DisplayName,
			})
			.ToListAsync()
			;

		// **************************************************
		var emptyItem =
			new ViewModels.Shared.IdNameViewModel<System.Guid?>
			(id: null, keyName: Resources.DataDictionary.SelectAnItem);

		list.Insert(index: 0, item: emptyItem);
		// **************************************************

		var result =
			new Microsoft.AspNetCore.Mvc.Rendering
			.SelectList(items: list, selectedValue: selectedValue,
			dataTextField: ViewModels.Shared.IdNameViewModel<int>.DataTextField,
			dataValueField: ViewModels.Shared.IdNameViewModel<int>.DataValueField);

		return result;
	}
	#endregion /GetLayoutsAsync()

	#region GetCulturesAsync()
	public static async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.Rendering.SelectList> GetCulturesAsync
		(Persistence.DatabaseContext databaseContext, object? selectedValue = null)
	{
		var list =
			await
			databaseContext.Cultures

			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Lcid)

			.Select(current => new ViewModels.Shared.IdNameViewModel<System.Guid?>
			{
				Id = current.Id,
				KeyName = current.DisplayName,
			})
			.ToListAsync()
			;

		// **************************************************
		var emptyItem =
			new ViewModels.Shared.IdNameViewModel<System.Guid?>
			(id: null, keyName: Resources.DataDictionary.SelectAnItem);

		list.Insert(index: 0, item: emptyItem);
		// **************************************************

		var result =
			new Microsoft.AspNetCore.Mvc.Rendering
			.SelectList(items: list, selectedValue: selectedValue,
			dataTextField: ViewModels.Shared.IdNameViewModel<int>.DataTextField,
			dataValueField: ViewModels.Shared.IdNameViewModel<int>.DataValueField);

		return result;
	}
	#endregion /GetCulturesAsync

	#region GetUsersForContactUsPageAsync()
	public static async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.Rendering.SelectList> GetUsersForContactUsPageAsync
		(Persistence.DatabaseContext databaseContext, object? selectedValue = null)
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var list =
			await
			databaseContext.LocalizedUsers

			.Where(current => current.User!.IsActive)
			.Where(current => current.User!.IsDeleted == false)
			.Where(current => current.User!.IsVisibleInContactUsPage)

			.Where(current => current.Culture!.Lcid == currentUICultureLcid)

			.OrderBy(current => current.User!.Ordering)
			.ThenBy(current => current.TitleInContactUsPage)

			.Select(current => new ViewModels.Shared.IdNameViewModel<System.Guid?>
			{
				Id = current.User!.Id,
				KeyName = current.TitleInContactUsPage,
			})
			.ToListAsync()
			;

		// **************************************************
		var emptyItem =
			new ViewModels.Shared.IdNameViewModel<System.Guid?>
			(id: null, keyName: Resources.DataDictionary.SelectAnItem);

		list.Insert(index: 0, item: emptyItem);
		// **************************************************

		var result =
			new Microsoft.AspNetCore.Mvc.Rendering
			.SelectList(items: list, selectedValue: selectedValue,
			dataTextField: ViewModels.Shared.IdNameViewModel<int>.DataTextField,
			dataValueField: ViewModels.Shared.IdNameViewModel<int>.DataValueField);

		return result;
	}
	#endregion /GetUsersForContactUsPageAsync()



	#region GetActiveItemsAsync()
	public static async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.Rendering.SelectList> GetActiveItemsAsync(Persistence.DatabaseContext databaseContext,
		Domain.Features.Common.Enums.BaseTableEnum baseTableEnum, object? selectedValue = null, int? maxIncludeCode = null)
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var list =
			databaseContext.BaseTableItems
			.Where(current => current.IsActive)
			.Where(current => current.BaseTable != null
				&& current.BaseTable.Code == baseTableEnum)
			;

		if (maxIncludeCode.HasValue)
		{
			list = list
				.Where(current => maxIncludeCode != null
					&& current.Code != null && current.Code <= maxIncludeCode)
				;
		}

		var mainList =
			await
			list
			.Select(current => new ViewModels.Shared.IdNameViewModel<System.Guid?>
			{
				Id = current.Id,
				KeyName = current.KeyName,
				Ordering = current.Ordering,

#pragma warning disable CS8602
				Title =
					(current.LocalizedBaseTableItems == null
					||
					current.LocalizedBaseTableItems.Count == 0)
					?
					null
					:
					current.LocalizedBaseTableItems.FirstOrDefault
						(other => other.Culture != null && other.Culture.Lcid == currentUICultureLcid).Title,
#pragma warning restore CS8602
			})
			.ToListAsync()
			;

		var orderedList =
			mainList
			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Name)
			.ToList()
			;

		// **************************************************
		var emptyItem =
			new ViewModels.Shared.IdNameViewModel
			<System.Guid?>(title: Resources.DataDictionary.SelectAnItem);

		orderedList.Insert(index: 0, item: emptyItem);
		// **************************************************

		var result =
			new Microsoft.AspNetCore.Mvc.Rendering
			.SelectList(items: orderedList, selectedValue: selectedValue,
			dataTextField: ViewModels.Shared.IdNameViewModel<int>.DataTextField,
			dataValueField: ViewModels.Shared.IdNameViewModel<int>.DataValueField);

		return result;
	}
	#endregion /GetActiveItemsAsync()



	#region GetPostTypesForUserAsync()
	public static async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.Rendering.SelectList> GetPostTypesForUserAsync
		(Persistence.DatabaseContext databaseContext, object? selectedValue = null)
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var list =
			await
			databaseContext.PostTypes

			.Where(current => current.IsActive)

			.Where(current => current.Culture != null &&
				current.Culture.Lcid == currentUICultureLcid)

			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Name)

			.Select(current => new ViewModels.Shared.IdNameViewModel<System.Guid?>
			{
				Id = current.Id,
				KeyName = current.DisplayName,
			})
			.ToListAsync()
			;

		// **************************************************
		var emptyItem =
			new ViewModels.Shared.IdNameViewModel<System.Guid?>
			(id: null, keyName: Resources.DataDictionary.SelectAnItem);

		list.Insert(index: 0, item: emptyItem);
		// **************************************************

		var result =
			new Microsoft.AspNetCore.Mvc.Rendering
			.SelectList(items: list, selectedValue: selectedValue,
			dataTextField: ViewModels.Shared.IdNameViewModel<int>.DataTextField,
			dataValueField: ViewModels.Shared.IdNameViewModel<int>.DataValueField);

		return result;
	}
	#endregion /GetPostTypesForUserAsync()

	#region GetPostTypesForAdminAsync()
	public static async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.Rendering.SelectList> GetPostTypesForAdminAsync
		(Persistence.DatabaseContext databaseContext, object? selectedValue = null)
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var list =
			await
			databaseContext.PostTypes

			.Where(current => current.Culture != null &&
				current.Culture.Lcid == currentUICultureLcid)

			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Name)

			.Select(current => new ViewModels.Shared.IdNameViewModel<System.Guid?>
			{
				Id = current.Id,
				KeyName = current.DisplayName,
			})
			.ToListAsync()
			;

		// **************************************************
		var emptyItem =
			new ViewModels.Shared.IdNameViewModel<System.Guid?>
			(id: null, keyName: Resources.DataDictionary.SelectAnItem);

		list.Insert(index: 0, item: emptyItem);
		// **************************************************

		var result =
			new Microsoft.AspNetCore.Mvc.Rendering
			.SelectList(items: list, selectedValue: selectedValue,
			dataTextField: ViewModels.Shared.IdNameViewModel<int>.DataTextField,
			dataValueField: ViewModels.Shared.IdNameViewModel<int>.DataValueField);

		return result;
	}
	#endregion /GetPostTypesForAdminAsync()



	#region GetPostCategoriesForUserAsync()
	public static async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.Rendering.SelectList> GetPostCategoriesForUserAsync
		(Persistence.DatabaseContext databaseContext, object? selectedValue = null)
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var list =
			await
			databaseContext.PostCategories

			.Where(current => current.IsActive)

			.Where(current => current.Culture != null &&
				current.Culture.Lcid == currentUICultureLcid)

			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Name)

			.Select(current => new ViewModels.Shared.IdNameViewModel<System.Guid?>
			{
				Id = current.Id,
				KeyName = current.DisplayName,
			})
			.ToListAsync()
			;

		// **************************************************
		var emptyItem =
			new ViewModels.Shared.IdNameViewModel<System.Guid?>
			(id: null, keyName: Resources.DataDictionary.SelectAnItem);

		list.Insert(index: 0, item: emptyItem);
		// **************************************************

		var result =
			new Microsoft.AspNetCore.Mvc.Rendering
			.SelectList(items: list, selectedValue: selectedValue,
			dataTextField: ViewModels.Shared.IdNameViewModel<int>.DataTextField,
			dataValueField: ViewModels.Shared.IdNameViewModel<int>.DataValueField);

		return result;
	}
	#endregion /GetPostCategoriesForUserAsync()

	#region GetPostCategoriesForAdminAsync()
	public static async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.Rendering.SelectList> GetPostCategoriesForAdminAsync
		(Persistence.DatabaseContext databaseContext, object? selectedValue = null)
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var list =
			await
			databaseContext.PostCategories

			.Where(current => current.Culture != null &&
				current.Culture.Lcid == currentUICultureLcid)

			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Name)

			.Select(current => new ViewModels.Shared.IdNameViewModel<System.Guid?>
			{
				Id = current.Id,
				KeyName = current.DisplayName,
			})
			.ToListAsync()
			;

		// **************************************************
		var emptyItem =
			new ViewModels.Shared.IdNameViewModel<System.Guid?>
			(id: null, keyName: Resources.DataDictionary.SelectAnItem);

		list.Insert(index: 0, item: emptyItem);
		// **************************************************

		var result =
			new Microsoft.AspNetCore.Mvc.Rendering
			.SelectList(items: list, selectedValue: selectedValue,
			dataTextField: ViewModels.Shared.IdNameViewModel<int>.DataTextField,
			dataValueField: ViewModels.Shared.IdNameViewModel<int>.DataValueField);

		return result;
	}
	#endregion /GetPostCategoriesForAdminAsync()



	#region GetBooleansForSearchAsync()
	public static Microsoft.AspNetCore.Mvc.Rendering.SelectList
		GetBooleansForSearchAsync(object? selectedValue = null)
	{
		var list = new System.Collections.Generic
			.List<ViewModels.Shared.IdNameViewModel<bool?>>();

		// **************************************************
		var emptyItem =
			new ViewModels.Shared.IdNameViewModel<bool?>
			(id: null, keyName: Resources.DataDictionary.SelectAnItem);

		list.Add(item: emptyItem);
		// **************************************************

		// **************************************************
		var yesItem =
			new ViewModels.Shared.IdNameViewModel<bool?>
			(id: true, keyName: Resources.DataDictionary.Yes);

		list.Add(item: yesItem);
		// **************************************************

		// **************************************************
		var noItem =
			new ViewModels.Shared.IdNameViewModel<bool?>
			(id: false, keyName: Resources.DataDictionary.No);

		list.Add(item: noItem);
		// **************************************************

		var result =
			new Microsoft.AspNetCore.Mvc.Rendering
			.SelectList(items: list, selectedValue: selectedValue,
			dataTextField: ViewModels.Shared.IdNameViewModel<int>.DataTextField,
			dataValueField: ViewModels.Shared.IdNameViewModel<int>.DataValueField);

		return result;
	}
	#endregion /GetBooleansForSearchAsync()
}
