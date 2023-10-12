using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Ro.Http.Controller;

/// <summary>
/// 👇 Create a Carter module for the API
/// </summary>
public class Empty : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => "Hello from Carter!");
    }
}