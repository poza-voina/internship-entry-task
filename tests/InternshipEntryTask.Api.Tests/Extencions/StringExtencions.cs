using System.Text;

namespace InternshipEntryTask.Api.Tests.Extencions;

public static class StringExtensions
{
    public static StringContent ToJsonContent(this string json)
    {
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}