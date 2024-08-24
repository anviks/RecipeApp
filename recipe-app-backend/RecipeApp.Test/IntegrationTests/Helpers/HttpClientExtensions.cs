using System.Text;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using Microsoft.AspNetCore.Http;
using Xunit;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Helpers;

public static class HttpClientExtensions
{
    public static Task<HttpResponseMessage> SendAsync(
        this HttpClient client,
        IHtmlFormElement form,
        IHtmlElement submitButton,
        bool isMultipart = false,
        Stream? fileStream = null)
    {
        return client.SendAsync(form, submitButton, new Dictionary<string, string>(), isMultipart, fileStream);
    }

    public static Task<HttpResponseMessage> SendAsync(
        this HttpClient client,
        IHtmlFormElement form,
        IEnumerable<KeyValuePair<string, string>> formValues,
        bool isMultipart = false,
        Stream? fileStream = null)
    {
        IElement submitElement = Assert.Single(form.QuerySelectorAll("[type=submit]"));
        var submitButton = Assert.IsAssignableFrom<IHtmlElement>(submitElement);

        return client.SendAsync(form, submitButton, formValues, isMultipart, fileStream);
    }

    public static Task<HttpResponseMessage> SendAsync(
        this HttpClient client,
        IHtmlFormElement form,
        IHtmlElement submitButton,
        IEnumerable<KeyValuePair<string, string>> formValues,
        bool isMultipart = false,
        Stream? fileStream = null)
    {
        MultipartFormDataContent? multipartContent = null;
        if (isMultipart)
        {
            multipartContent = new MultipartFormDataContent();
        }

        foreach (var (key, value) in formValues)
        {
            switch (form[key])
            {
                case IHtmlInputElement inputElement:
                {
                    if (inputElement.Type == "checkbox" && bool.Parse(value))
                    {
                        inputElement.IsChecked = true;
                        multipartContent?.Add(new StringContent(value), key);
                    }
                    else if (inputElement.Type == "file" && isMultipart)
                    {
                        multipartContent!.Add(new StreamContent(fileStream!), key, value);
                    }
                    else
                    {
                        inputElement.Value = value;
                        multipartContent?.Add(new StringContent(value), key);
                    }

                    break;
                }
                case IHtmlSelectElement selectElement:
                {
                    selectElement.Value = value;
                    break;
                }
            }
        }

        DocumentRequest? submit = form.GetSubmission(submitButton);
        var target = (Uri)submit!.Target;
        if (submitButton.HasAttribute("formaction"))
        {
            var formaction = submitButton.GetAttribute("formaction");
            if (!string.IsNullOrEmpty(formaction))
                target = new Uri(formaction, UriKind.Relative);
        }

        var submission = new HttpRequestMessage(new HttpMethod(submit.Method.ToString()), target)
        {
            Content = /*isMultipart ? multipartContent! :*/ new StreamContent(submit.Body)
        };

        foreach (var (key, value) in submit.Headers)
        {
            submission.Headers.TryAddWithoutValidation(key, value);
            submission.Content.Headers.TryAddWithoutValidation(key, value);
        }

        return client.SendAsync(submission);
    }
}