using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
/// <summary>
/// Swagger фильтр для удаления plainText
/// </summary>
public class RemoveTextPlainResponseFilter : IOperationFilter
{
    /// <inheritdoc/>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Responses != null)
        {
            foreach (var response in operation.Responses.Values)
            {
                if (response.Content != null)
                {
                    response.Content.Remove("text/plain");
                }
            }
        }
    }
}
