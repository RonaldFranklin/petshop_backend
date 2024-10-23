using Carter;
using FluentValidation;
using PetShop.Modules.Scheduling.Interfaces;
using PetShop.Response;
using System.Net;
using System.Security.Claims;

namespace PetShop.Modules.Scheduling;

public class SchedulingEndpoints : CarterModule
{
    public SchedulingEndpoints() : base("schedulings") { }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/",
           async (
               ISchedulingService service,
               ClaimsPrincipal user
           ) =>
           {
               int.TryParse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);
               string? userRole = user?.FindFirst(ClaimTypes.Role)?.Value;
               int ownerId = userRole == "manager" ? 0 : userId;

               var response = await service.GetSchedulingsAsync(ownerId);

               return response.Match(
                   left => Results.Problem(left.Message, statusCode: (int)HttpStatusCode.InternalServerError),
                   right => Results.Ok(ApiResponse<List<SchedulingDTO>>.Ok(right))
               );
           })
       .RequireAuthorization()
       .WithName("GetSchedulings")
       .WithOpenApi();

        app.MapGet("/{id}",
           async (
               int id,
               ISchedulingService service,
               ClaimsPrincipal user
           ) =>
           {
               int.TryParse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);
               string? userRole = user?.FindFirst(ClaimTypes.Role)?.Value;
               int ownerId = userRole == "manager" ? 0 : userId;

               var response = await service.GetSchedulingByIdAsync(id, ownerId);

               return response.Match(
                   left => Results.Problem(left.Message, statusCode: (int)HttpStatusCode.InternalServerError),
                   right => Results.Ok(ApiResponse<SchedulingDTO>.Ok(right))
               );
           })
       .RequireAuthorization()
       .WithName("GetSchedulingsById")
       .WithOpenApi();

        app.MapPost("/",
            async (
                SchedulingDTO request,
                IValidator<SchedulingDTO> validator,
                ISchedulingService service,
                ClaimsPrincipal user
            ) =>
            {
                var requestValidator = await validator.ValidateAsync(request);

                if (!requestValidator.IsValid)
                    return Results.ValidationProblem(requestValidator.ToDictionary());

                int.TryParse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);
                string? userRole = user?.FindFirst(ClaimTypes.Role)?.Value;
                int ownerId = userRole == "manager" ? 0 : userId;

                var response = await service.InsertSchedulingAsync(request, ownerId);

                return response.Match(
                    left => Results.Problem(left.Message, statusCode: (int)HttpStatusCode.InternalServerError),
                    right => Results.Ok(ApiResponse<SchedulingDTO>.Ok(right))
                );
            })
        .RequireAuthorization()
        .WithName("InsertSchedulings")
        .WithOpenApi();

        app.MapPut("/{id}",
            async (
                int id,
                SchedulingDTO request,
                IValidator<SchedulingDTO> validator,
                ISchedulingService service,
                ClaimsPrincipal user
            ) =>
            {
                var requestValidator = await validator.ValidateAsync(request);

                if (!requestValidator.IsValid)
                    return Results.ValidationProblem(requestValidator.ToDictionary());

                int.TryParse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);
                string? userRole = user?.FindFirst(ClaimTypes.Role)?.Value;
                int ownerId = userRole == "manager" ? 0 : userId;

                var response = await service.UpdateSchedulingAsync(id, request, ownerId);

                return response.Match(
                    left => Results.Problem(left.Message, statusCode: (int)HttpStatusCode.InternalServerError),
                    right => Results.Ok(ApiResponse<SchedulingDTO>.Ok(right))
                );
            })
        .RequireAuthorization()
        .WithName("UpdateSchedulings")
        .WithOpenApi();

        app.MapDelete("/{id}",
           async (
               int id,
               ISchedulingService service,
               ClaimsPrincipal user
           ) =>
           {
               int.TryParse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);
               string? userRole = user?.FindFirst(ClaimTypes.Role)?.Value;
               int ownerId = userRole == "manager" ? 0 : userId;

               await service.DeleteSchedulingAsync(id, ownerId);
               return Results.NoContent();
           })
       .RequireAuthorization()
       .WithName("DeleteSchedulings")
       .WithOpenApi();
    }
}
