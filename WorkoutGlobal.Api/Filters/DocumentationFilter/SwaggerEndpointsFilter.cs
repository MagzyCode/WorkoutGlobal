using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WorkoutGlobal.Api.Attributes;

namespace WorkoutGlobal.Api.Filters.DocumentationFilter
{
    public class SwaggerEndpointsFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var description in context.ApiDescriptions)
            {
                string relativePath = description.RelativePath;

                description.TryGetMethodInfo(out var methodInfo);

                var testAttribute = methodInfo.GetCustomAttributes(true)
                    .OfType<TestApiAttribute>()
                    .Distinct();

                if (testAttribute.Any())
                {
                    var removeRoutes = swaggerDoc.Paths
                        .Where(x => x.Key.ToLower().Contains(relativePath.ToLower()))
                        .ToList();

                    removeRoutes.ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });
                }
            }
        }
    }
}
