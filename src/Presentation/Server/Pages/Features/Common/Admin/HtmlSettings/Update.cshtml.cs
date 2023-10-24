using System.Linq;
using Dtat;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Common.Admin.HtmlSettings;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.Administrator)]
public class UpdateModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public UpdateModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Common.Admin.HtmlSettings.UpdateViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
	{
		var result =
			await
			DatabaseContext.HtmlSettings

			.Select(current => new ViewModels.Pages
				.Features.Common.Admin.HtmlSettings.UpdateViewModel()
			{
				Id = current.Id,

				ToastDelayStep = current.ToastDelayStep,
				ToastCssClasses = current.ToastCssClasses,
				TableCssClasses = current.TableCssClasses,
				ToastInitialDelay = current.ToastInitialDelay,
				TableFooterCssClasses = current.TableFooterCssClasses,
				TableHeaderCssClasses = current.TableHeaderCssClasses,
				DisplayDateTimeFormat = current.DisplayDateTimeFormat,
				CardsContainerCssClasses = current.CardsContainerCssClasses,
				PicturePreviewExtensions = current.PicturePreviewExtensions,
				PermittedFileExtensionsForUploading = current.PermittedFileExtensionsForUploading,
			})
			.FirstOrDefaultAsync();

		if (result is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		ViewModel = result;

		return Page();
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid == false)
		{
			return Page();
		}

		// **************************************************
		var foundedItem =
			await
			DatabaseContext.HtmlSettings
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		foundedItem.SetUpdateDateTime();

		foundedItem.DisplayDateTimeFormat =
			ViewModel.DisplayDateTimeFormat.Fix()!;

		foundedItem.ToastDelayStep = ViewModel.ToastDelayStep;
		foundedItem.ToastInitialDelay = ViewModel.ToastInitialDelay;

		foundedItem.ToastCssClasses = ViewModel.ToastCssClasses.Fix();
		foundedItem.TableCssClasses = ViewModel.TableCssClasses.Fix();
		foundedItem.TableFooterCssClasses = ViewModel.TableFooterCssClasses.Fix();
		foundedItem.TableHeaderCssClasses = ViewModel.TableHeaderCssClasses.Fix();
		foundedItem.CardsContainerCssClasses = ViewModel.CardsContainerCssClasses.Fix();
		foundedItem.PicturePreviewExtensions = ViewModel.PicturePreviewExtensions.Fix();
		foundedItem.PermittedFileExtensionsForUploading = ViewModel.PermittedFileExtensionsForUploading.Fix();
		// **************************************************

		await DatabaseContext.SaveChangesAsync();

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Updated,
			arg0: Resources.DataDictionary.Data);

		AddPageSuccess(message: successMessage);
		// **************************************************

		return Page();
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
