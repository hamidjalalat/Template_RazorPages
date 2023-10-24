﻿using Dtat;
using System.Linq;

namespace Infrastructure;

public static class HtmlHelpers : object
{
	static HtmlHelpers()
	{
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatDisplayString
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, string? value)
	{
		if (string.IsNullOrWhiteSpace(value: value))
		{
			return html.Raw
				(value: Constants.Format.NullValue);
		}

		var result = value.Fix();

		return html.Raw(value: result);
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatDisplayInteger
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, long? value)
	{
		if (value is null)
		{
			return html.Raw
				(value: Constants.Format.NullValue);
		}

		var result =
			value.Value.ToString
			(format: Constants.Format.Integer);

		result =
			result.ConvertDigitsToUnicode();

		return html.Raw(value: result);
	}

	public static Microsoft.AspNetCore.Html
		.IHtmlContent DtatDisplayRowNumberWithTd
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, long? value)
	{
		var td =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "td");

		td.AddCssClass(value: "text-center");

		var innerHtml =
			DtatDisplayInteger(html: html, value: value);

		td.InnerHtml.AppendHtml(content: innerHtml);

		return td;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatDisplayInlineBoolean
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, bool? value)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var result =
			"<i class='bi bi-square'></i>";

		if (value is not null && value.Value)
		{
			result =
				"<i class='bi bi bi-check-square'></i>";
		}

		return html.Raw(value: result);

		//var input =
		//	new Microsoft.AspNetCore.Mvc
		//	.Rendering.TagBuilder(tagName: "input");

		//input.Attributes.Add
		//	(key: "type", value: "checkbox");

		//input.Attributes.Add
		//	(key: "disabled", value: "disabled");

		//if (value is not null && value.Value)
		//{
		//	input.Attributes.Add
		//		(key: "checked", value: "checked");
		//}

		//return input;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatDisplayBoolean
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, bool? value)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var div = new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "div");

		var innerHtml =
			"<i class='bi bi-square'></i>";

		if (value is not null && value.Value)
		{
			innerHtml =
				"<i class='bi bi bi-check-square'></i>";
		}

		div.InnerHtml.AppendHtml(encoded: innerHtml);

		//var input =
		//	new Microsoft.AspNetCore.Mvc
		//	.Rendering.TagBuilder(tagName: "input");

		//input.Attributes.Add
		//	(key: "type", value: "checkbox");

		//input.Attributes.Add
		//	(key: "disabled", value: "disabled");

		//if (value is not null && value.Value)
		//{
		//	input.Attributes.Add
		//		(key: "checked", value: "checked");
		//}

		//div.InnerHtml.AppendHtml(content: input);

		return div;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatDisplayStringWithTd
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, string? value, bool? isLeftToRight = null)
	{
		var td =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "td");

		if (isLeftToRight is not null && isLeftToRight.Value)
		{
			td.Attributes.Add
				(key: "dir", value: "ltr");
		}

		var innerHtml =
			DtatDisplayString(html: html, value: value);

		td.InnerHtml.AppendHtml(content: innerHtml);

		return td;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatDisplayBooleanWithTd
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, bool? value)
	{
		var td =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "td");

		td.AddCssClass(value: "text-center");

		var innerHtml =
			DtatDisplayBoolean(html: html, value: value);

		td.InnerHtml.AppendHtml(content: innerHtml);

		return td;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatDisplayIntegerWithTd
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, long? value)
	{
		var td =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "td");

		td.Attributes.Add
			(key: "dir", value: "ltr");

		var innerHtml =
			DtatDisplayInteger(html: html, value: value);

		td.InnerHtml.AppendHtml(content: innerHtml);

		return td;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatDisplayDateTime
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, System.DateTime? value)
	{
		if (value is null)
		{
			return html.Raw
				(value: Constants.Format.NullValue);
		}

		var result =
			value.Value.ToString
			(format: Constants.Format.DateTime);

		result =
			result.ConvertDigitsToUnicode();

		return html.Raw(value: result);
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatDisplayDateOffset
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, System.DateTimeOffset? value)
	{
		if (value is null)
		{
			return html.Raw
				(value: Constants.Format.NullValue);
		}

		var result =
			value.Value.ToString
			(format: Constants.Format.Date);

		result =
			result.ConvertDigitsToUnicode();

		return html.Raw(value: result);
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatDisplayDateTimeOffset
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, System.DateTimeOffset? value)
	{
		if (value is null)
		{
			return html.Raw
				(value: Constants.Format.NullValue);
		}

		var result =
			value.Value.ToString
			(format: Constants.Format.DateTime);

		result =
			result.ConvertDigitsToUnicode();

		return html.Raw(value: result);
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatDisplayDateTimeWithTd
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, System.DateTime? value)
	{
		var td =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "td");

		td.Attributes.Add
			(key: "dir", value: "ltr");

		var innerHtml =
			DtatDisplayDateTime(html: html, value: value);

		td.InnerHtml.AppendHtml(content: innerHtml);

		return td;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatDisplayDateTimeOffsetWithTd
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, System.DateTimeOffset? value)
	{
		var td =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "td");

		td.Attributes.Add
			(key: "dir", value: "ltr");

		var innerHtml =
			DtatDisplayDateTimeOffset(html: html, value: value);

		td.InnerHtml.AppendHtml(content: innerHtml);

		return td;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatGetLinkCaptionForList
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconList();

		var span =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "span");

		span.AddCssClass(value: "mx-1");

		span.InnerHtml.Append(unencoded: "[");
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.AppendHtml(content: icon);
		span.InnerHtml.Append(unencoded: Resources.ButtonCaptions.BackToList);
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.Append(unencoded: "]");

		return span;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatGetLinkCaptionForDetails
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconDetails();

		var span =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "span");

		span.AddCssClass(value: "mx-1");

		span.InnerHtml.Append(unencoded: "[");
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.AppendHtml(content: icon);
		span.InnerHtml.Append(unencoded: Resources.ButtonCaptions.Details);
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.Append(unencoded: "]");

		return span;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatGetLinkCaptionForCreate
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconCreate();

		var span =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "span");

		//span.AddCssClass(value: "mx-1");

		//span.InnerHtml.Append(unencoded: "[");
		//span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.AppendHtml(content: icon);
		span.InnerHtml.Append(unencoded: Resources.ButtonCaptions.Create);
		//span.InnerHtml.Append(unencoded: " ");
		//span.InnerHtml.Append(unencoded: "]");

		return span;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatGetLinkCaptionForUpdate
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconUpdate();

		var span =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "span");

		span.AddCssClass(value: "mx-1");

		span.InnerHtml.Append(unencoded: "[");
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.AppendHtml(content: icon);
		span.InnerHtml.Append(unencoded: Resources.ButtonCaptions.Update);
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.Append(unencoded: "]");

		return span;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatGetLinkCaptionForUpdateInformationsAgain
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconReset();

		var span =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "span");

		span.AddCssClass(value: "mx-1");

		span.InnerHtml.Append(unencoded: "[");
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.AppendHtml(content: icon);
		span.InnerHtml.Append(unencoded: Resources.ButtonCaptions.UpdateInformationsAgain);
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.Append(unencoded: "]");

		return span;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatGetLinkCaptionForDelete
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconDelete();

		var span =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "span");

		span.AddCssClass(value: "mx-1");

		span.InnerHtml.Append(unencoded: "[");
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.AppendHtml(content: icon);
		span.InnerHtml.Append(unencoded: Resources.ButtonCaptions.Delete);
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.Append(unencoded: "]");

		return span;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatGetIconDetails
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconDetails();

		return icon;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatGetIconDisplay
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconDisplay();

		return icon;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatGetIconCreate
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconCreate();

		return icon;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatGetIconUpdate
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconUpdate();

		return icon;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatGetIconDelete
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconDelete();

		return icon;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatGetIconCustom
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, string iconName)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconCustom(iconName: iconName);

		return icon;
	}

	public static Microsoft.AspNetCore.Mvc.Rendering.SelectList DtatGetEnumSelectList<TEnum>
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, int? selectedValue = null) where TEnum : struct
	{
		var list =
			html.GetEnumSelectList<TEnum>().ToList();

		// **************************************************
		var emptySelectListItem =
			new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
			(text: Resources.DataDictionary.SelectAnItem, value: string.Empty);

		list.Insert(index: 0, item: emptySelectListItem);
		// **************************************************

		var result =
			new Microsoft.AspNetCore.Mvc.Rendering
			.SelectList(items: list, selectedValue: selectedValue,
			dataTextField: nameof(Microsoft.AspNetCore.Mvc.Rendering.SelectListItem.Text),
			dataValueField: nameof(Microsoft.AspNetCore.Mvc.Rendering.SelectListItem.Value));

		return result;
	}
}
