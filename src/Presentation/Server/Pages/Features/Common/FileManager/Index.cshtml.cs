using System.Linq;

namespace Server.Pages.Features.Common.FileManager;

[Infrastructure.Security.CustomAuthorize(minRoleCode:
	Domain.Features.Identity.Enums.RoleEnum.SpecialUser)]
public class IndexModel :
	Infrastructure.BasePageModel
{
	#region Constructor
	public IndexModel
		(Microsoft.Extensions.Hosting.IHostEnvironment hostEnvironment,
		Services.Features.Common.HtmlSettingService htmlSettingService,
		Infrastructure.Security.AuthenticatedUserService authenticatedUserService) : base()
	{
		HostEnvironment = hostEnvironment;
		HtmlSettingService = htmlSettingService;
		AuthenticatedUserService = authenticatedUserService;

		PageAddress =
			"/Features/Common/FileManager/Index";

		switch (authenticatedUserService.RoleCode)
		{
			case Domain.Features.Identity.Enums.RoleEnum.SimpleUser:
			{
				throw new System.Exception
					(message: "Access Denied!");
			}

			case Domain.Features.Identity.Enums.RoleEnum.SpecialUser:
			{
				//var userId =
				//	authenticatedUserService.UserId;

				//if (userId is null)
				//{
				//	throw new System.Exception
				//		(message: "Access Denied!");
				//}

				var userId =
					authenticatedUserService.UserId ??
					throw new System.Exception(message: "Access Denied!");

				var path =
					$"{HostEnvironment.ContentRootPath}\\wwwroot\\images";

				if (System.IO.Directory.Exists(path: path) == false)
				{
					System.IO.Directory.CreateDirectory(path: path);
				}

				path = $"{path}\\users";

				if (System.IO.Directory.Exists(path: path) == false)
				{
					System.IO.Directory.CreateDirectory(path: path);
				}

				var userIdString = userId.ToString()
					.Replace(oldValue: "-", newValue: string.Empty);

				path = $"{path}\\{userIdString}";

				if (System.IO.Directory.Exists(path: path) == false)
				{
					System.IO.Directory.CreateDirectory(path: path);
				}

				PhysicalRootPath = path;
				Prefix = $"/images/users/{userIdString}";

				break;
			}

			case Domain.Features.Identity.Enums.RoleEnum.Supervisor:
			{
				Prefix = "/images";

				PhysicalRootPath =
					$"{HostEnvironment.ContentRootPath}\\wwwroot\\images";

				break;
			}

			case Domain.Features.Identity.Enums.RoleEnum.Programmer:
			case Domain.Features.Identity.Enums.RoleEnum.Administrator:
			case Domain.Features.Identity.Enums.RoleEnum.ApplicationOwner:
			{
				Prefix =
					string.Empty;

				PhysicalRootPath =
					$"{HostEnvironment.ContentRootPath}\\wwwroot";

				break;
			}

			default:
			{
				throw new System.Exception
					(message: "Access Denied!");
			}
		}

		Files = new System.Collections
			.Generic.List<System.IO.FileInfo>();

		Directories = new System.Collections
			.Generic.List<System.IO.DirectoryInfo>();
	}
	#endregion /Constructor

	#region Properties

	public string? CurrentPath { get; set; }
	public string? PhysicalCurrentPath { get; set; }
	public System.Collections.Generic.IList<System.IO.FileInfo> Files { get; set; }
	public System.Collections.Generic.IList<System.IO.DirectoryInfo> Directories { get; set; }

	#endregion /Properties

	#region Read Only Properties

	public string Prefix { get; init; }
	public string PageAddress { get; init; }
	public string PhysicalRootPath { get; init; }
	public Microsoft.Extensions.Hosting.IHostEnvironment HostEnvironment { get; init; }
	public Services.Features.Common.HtmlSettingService HtmlSettingService { get; init; }
	public Infrastructure.Security.AuthenticatedUserService AuthenticatedUserService { get; init; }

	#endregion /Read Only Properties

	#region OnGet
	public void OnGet(string? path)
	{
		try
		{
			CheckPathAndSetCurrentPath(path: path);

			SetDirectoriesAndFiles();
		}
		catch (System.Exception ex)
		{
			AddToastError
				(message: ex.Message);
		}
	}
	#endregion /OnGet

	#region OnPostDeleteItems
	public void OnPostDeleteItems
		(string? path, System.Collections.Generic.IList<string>? items)
	{
		try
		{
			CheckPathAndSetCurrentPath(path: path);

			if (items is null || items.Count == 0)
			{
				var errorMessage =
					"You did not select any files or folders for deleting!";

				AddToastError
					(message: errorMessage);

				SetDirectoriesAndFiles();
				return;
			}

			foreach (var item in items)
			{
				try
				{
					var physicalItemPath =
						$"{PhysicalRootPath}{CurrentPath}{item}"
						.Replace(oldValue: "/", newValue: "\\");

					if (System.IO.Directory.Exists(path: physicalItemPath))
					{
						System.IO.Directory.Delete
							(path: physicalItemPath, recursive: true);

						var successMessage =
							$"The direcotry ({item}) deleted successfully.";

						AddToastSuccess
							(message: successMessage);
					}
					else
					{
						if (System.IO.File.Exists(path: physicalItemPath))
						{
							// نوشتن دو دستور ذیل بسیار اهمیت دارد
							System.GC.Collect();
							System.GC.WaitForPendingFinalizers();

							System.IO.File.Delete
								(path: physicalItemPath);

							var successMessage =
								$"The file ({item}) deleted successfully.";

							AddToastSuccess
								(message: successMessage);
						}
					}
				}
				catch (System.Exception ex)
				{
					AddToastError
						(message: ex.Message);
				}
			}

			SetDirectoriesAndFiles();
		}
		catch (System.Exception ex)
		{
			AddToastError
				(message: ex.Message);
		}
	}
	#endregion /OnPostDeleteItems

	#region OnPostCreateDirectory
	public void OnPostCreateDirectory
		(string? path, string? directoryName)
	{
		try
		{
			CheckPathAndSetCurrentPath(path: path);

			if (string.IsNullOrWhiteSpace(value: directoryName))
			{
				SetDirectoriesAndFiles();
				return;
			}

			directoryName = directoryName
				.Replace(oldValue: " ", newValue: string.Empty);

			var physicalPath =
				$"{PhysicalRootPath}{CurrentPath}{directoryName}"
				.Replace(oldValue: "/", newValue: "\\");

			if (System.IO.Directory.Exists(path: physicalPath))
			{
				// **************************************************
				var errorMessage =
					$"The [{directoryName}] folder already exists!";

				AddPageError
					(message: errorMessage);
				// **************************************************

				SetDirectoriesAndFiles();
				return;
			}

			System.IO.Directory
				.CreateDirectory(path: physicalPath);

			// **************************************************
			var successMessage =
				$"The [{directoryName}] folder has been created successfully.";

			AddToastSuccess
				(message: successMessage);
			// **************************************************

			SetDirectoriesAndFiles();
		}
		catch (System.Exception ex)
		{
			AddToastError
				(message: ex.Message);
		}
	}
	#endregion /OnPostCreateDirectory

	#region OnPostUploadFilesAsync
	public async System.Threading.Tasks.Task
		OnPostUploadFilesAsync
		(string? path, System.Collections.Generic
		.List<Microsoft.AspNetCore.Http.IFormFile> files)
	{
		try
		{
			CheckPathAndSetCurrentPath(path: path);

			if (files is null || files.Count == 0)
			{
				var errorMessage =
					"You did not specify any files for uploading!";

				AddToastError
					(message: errorMessage);
			}
			else
			{
				foreach (var file in files)
				{
					await CheckFileValidationAndSaveAsync
						(overwriteIfFileExists: true, file: file);
				}
			}
		}
		catch (System.Exception ex)
		{
			AddToastError
				(message: ex.Message);
		}

		SetDirectoriesAndFiles();
	}
	#endregion /OnPostUploadFilesAsync

	#region CheckFileValidationAndSaveAsync
	private async System.Threading.Tasks.Task<bool>
		CheckFileValidationAndSaveAsync
		(bool overwriteIfFileExists, Microsoft.AspNetCore.Http.IFormFile? file)
	{
		var result =
			await
			CheckFileValidationAsync(file: file);

		if (result == false)
		{
			return false;
		}

		var fileName =
			file!.FileName.Trim()
			.Replace(oldValue: " ", newValue: "_");

		var physicalPathName =
			$"{PhysicalRootPath}{CurrentPath}{fileName}"
			.Replace(oldValue: "/", newValue: "\\");

		if (overwriteIfFileExists == false)
		{
			if (System.IO.File.Exists(path: physicalPathName))
			{
				var errorMessage =
					$"File '{fileName}' already exists!";

				AddToastError
					(message: errorMessage);

				return false;
			}
		}

		using (var stream = System.IO.File.Create(path: physicalPathName))
		{
			await file.CopyToAsync(target: stream);

			await stream.FlushAsync();

			stream.Close();
		}

		if (string.Compare(file.FileName, fileName, ignoreCase: true) == 0)
		{
			var successMessage =
				$"File '{fileName}' uploaded successfully.";

			AddToastSuccess
				(message: successMessage);
		}
		else
		{
			var successMessage =
				$"File '{file.FileName}' with the name of '{fileName}' uploaded successfully.";

			AddToastSuccess
				(message: successMessage);
		}

		return true;
	}
	#endregion /CheckFileValidationAndSaveAsync

	#region CheckFileValidation
	private async System.Threading.Tasks.Task<bool>
		CheckFileValidationAsync(Microsoft.AspNetCore.Http.IFormFile? file)
	{
		if (file is null)
		{
			var errorMessage =
				"You did not specify any files for uploading!";

			AddToastError
				(message: errorMessage);

			return false;
		}

		if (file.Length == 0)
		{
			var errorMessage =
				$"The file ({file.FileName}) did not uploaded successfully!";

			AddToastError
				(message: errorMessage);

			return false;
		}

		var fileExtension =
			System.IO.Path.GetExtension
			(path: file.FileName)?.ToLower();

		if (fileExtension is null)
		{
			var errorMessage =
				$"The file ({file.FileName}) does not have any extension!";

			AddToastError
				(message: errorMessage);

			return false;
		}

		var HtmlSetting =
			await
			HtmlSettingService.GetInstanceAsync();

		var permittedFileExtensions =
			HtmlSetting.PermittedFileExtensionsForUploadingArray;

		if (permittedFileExtensions is null)
		{
			var errorMessage =
				$"The file ({file.FileName}) does not have a valid extension!";

			AddToastError
				(message: errorMessage);

			return false;
		}

		var permittedFileExtensionsList =
			permittedFileExtensions.ToList();

		if (permittedFileExtensionsList.Contains(item: fileExtension) == false)
		{
			var errorMessage =
				$"The file ({file.FileName}) does not have a valid extension!";

			AddToastError
				(message: errorMessage);

			return false;
		}

		return true;
	}
	#endregion /CheckFileValidation

	#region CheckPathAndSetCurrentPath
	/// <summary>
	/// قانون
	///
	/// CurrentPath:
	///		/
	///		/Images/
	///
	/// یعنی همیشه دو طرف آن / دارد
	/// </summary>
	public void CheckPathAndSetCurrentPath(string? path)
	{
		// **************************************************
		var fixedPath = "/";

		if (string.IsNullOrWhiteSpace(value: path) == false)
		{
			fixedPath = path.Replace
				(oldValue: "\\", newValue: "/");

			if (fixedPath.StartsWith(value: "/") == false)
			{
				fixedPath = $"/{fixedPath}";
			}

			if (fixedPath.EndsWith(value: "/") == false)
			{
				fixedPath = $"{fixedPath}/";
			}

			while (fixedPath.Contains(value: "//"))
			{
				fixedPath = fixedPath.Replace
					(oldValue: "//", newValue: "/");
			}
		}
		// **************************************************

		CurrentPath = fixedPath;

		PhysicalCurrentPath =
			$"{PhysicalRootPath}{CurrentPath}"
			.Replace("/", "\\");

		if (System.IO.Directory.Exists(path: PhysicalCurrentPath) == false)
		{
			CurrentPath = "/";
			PhysicalCurrentPath = PhysicalRootPath;
		}
	}
	#endregion /CheckPathAndSetCurrentPath

	#region SetDirectoriesAndFiles
	public void SetDirectoriesAndFiles()
	{
		if (string.IsNullOrWhiteSpace(value: PhysicalCurrentPath) ||
			System.IO.Directory.Exists(path: PhysicalCurrentPath) == false)
		{
			Files = new System.Collections.Generic.List<System.IO.FileInfo>();
			Directories = new System.Collections.Generic.List<System.IO.DirectoryInfo>();

			return;
		}

		var directoryInfo = new System.IO
			.DirectoryInfo(path: PhysicalCurrentPath);

		Files =
			directoryInfo.GetFiles()
			.OrderBy(current => current.Extension)
			.ThenBy(current => current.Name)
			.ToList()
			;

		Directories =
			directoryInfo.GetDirectories()
			.OrderBy(current => current.Name)
			.ToList()
			;
	}
	#endregion /SetDirectoriesAndFiles
}
