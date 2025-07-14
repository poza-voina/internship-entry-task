using System.Text;
using System.Text.Json;

namespace InternshipEntryTask.Api.Tests.Base;

public class HttpRequestBuilder
{
    private const string CONTENT_TYPE = "application/json";
    private readonly HttpRequestMessage _request;

    public HttpRequestBuilder(HttpMethod method, string url)
    {
        _request = new HttpRequestMessage(method, url);
    }

    public HttpRequestBuilder WithContent(HttpContent content)
    {
        _request.Content = content;
        return this;
    }

    public HttpRequestBuilder WithJsonContent<T>(T obj)
    {
        var json = JsonSerializer.Serialize(obj);
        _request.Content = new StringContent(json, Encoding.UTF8, CONTENT_TYPE);
        return this;
    }

    public HttpRequestBuilder WithHeader(string name, string value)
    {
        _request.Headers.Add(name, value);
        return this;
    }

    public HttpRequestMessage Build() => _request;
}