﻿namespace Infrastructure.TagHelpers.Full;

[Microsoft.AspNetCore.Razor.TagHelpers
	.HtmlTargetElement(tag: Constants.TagHelper.FullTextArea,
	TagStructure = Microsoft.AspNetCore.Razor.TagHelpers.TagStructure.WithoutEndTag)]
public class FullTextAreaTagHelper :
	Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
{
	public FullTextAreaTagHelper
		(Microsoft.AspNetCore.Mvc.ViewFeatures.IHtmlGenerator generator) : base()
	{
		Generator = generator;
	}

	private Microsoft.AspNetCore.Mvc.ViewFeatures.IHtmlGenerator Generator { get; }

	[Microsoft.AspNetCore.Mvc.ViewFeatures.ViewContext]
	[Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeNotBound]
	public Microsoft.AspNetCore.Mvc.Rendering.ViewContext? ViewContext { get; set; }

	[Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName(name: "asp-for")]
	public Microsoft.AspNetCore.Mvc.ViewFeatures.ModelExpression? For { get; set; }

	public override async System.Threading.Tasks.Task ProcessAsync
		(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext context,
		Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput output)
	{
		if (For is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(For));
		}

		if (ViewContext is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(ViewContext));
		}

		// **************************************************
		var div =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "div");

		div.AddCssClass(value: "mb-3");
		// **************************************************

		// **************************************************
		var label =
			await
			Utility.GenerateLabelAsync
			(generator: Generator, viewContext: ViewContext, @for: For);

		div.InnerHtml.AppendHtml(encoded: label);
		// **************************************************

		// **************************************************
		string? dirString = null;

		var dirAttribute =
			output.Attributes["dir"];

		if (dirAttribute is not null)
		{
			var dirValue =
				dirAttribute.Value;

			if (dirValue is not null)
			{
				dirString =
					dirValue.ToString()?
					.Replace(oldValue: "{", newValue: string.Empty)
					.Replace(oldValue: "}", newValue: string.Empty);
			}
		}
		// **************************************************

		// **************************************************
		var textBox =
			await
			Utility.GenerateTextAreaAsync(generator: Generator,
			viewContext: ViewContext, @for: For, dir: dirString);

		div.InnerHtml.AppendHtml(encoded: textBox);
		// **************************************************

		// **************************************************
		var validationMessage =
			await
			Utility.GenerateValidationMessageAsync
			(generator: Generator, viewContext: ViewContext, @for: For);

		div.InnerHtml.AppendHtml(encoded: validationMessage);
		// **************************************************

		// **************************************************
		output.TagName = null;

		output.TagMode =
			Microsoft.AspNetCore.Razor
			.TagHelpers.TagMode.StartTagAndEndTag;

		output.Content.SetHtmlContent(htmlContent: div);
		// **************************************************
	}
}