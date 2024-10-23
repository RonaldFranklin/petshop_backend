using Carter;
using FluentValidation;
using PetShop.Modules.Services.Interfaces;
using PetShop.Response;
using System.Net;

namespace PetShop.Modules.Services;

public class ServiceEndpoints : CarterModule
{
    public ServiceEndpoints() : base("services") { }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/",
           async (
               IServiceService service
           ) =>
           {
               var response = await service.GetServicesAsync();

               return response.Match(
                   left => Results.Problem(left.Message, statusCode: (int)HttpStatusCode.InternalServerError),
                   right => Results.Ok(ApiResponse<List<ServiceDTO>>.Ok(right))
               );
           })
       .RequireAuthorization()
       .WithName("GetServices")
       .WithOpenApi();

        app.MapGet("/{id}",
           async (
               int id,
               IServiceService service
           ) =>
           {
               var response = await service.GetServiceByIdAsync(id);

               return response.Match(
                   left => Results.Problem(left.Message, statusCode: (int)HttpStatusCode.InternalServerError),
                   right => Results.Ok(ApiResponse<ServiceDTO>.Ok(right))
               );
           })
       .RequireAuthorization("Admin")
       .WithName("GetServicesById")
       .WithOpenApi();

        app.MapPost("/", 
            async (
                ServiceDTO request,
                IValidator<ServiceDTO> validator,
                IServiceService service
            ) =>
            {
                var requestValidator = await validator.ValidateAsync(request);

                if (!requestValidator.IsValid)
                    return Results.ValidationProblem(requestValidator.ToDictionary());

                var response = await service.InsertServiceAsync(request);

                return response.Match(
                    left => Results.Problem(left.Message, statusCode: (int)HttpStatusCode.InternalServerError), 
                    right => Results.Ok(ApiResponse<ServiceDTO>.Ok(right))                                     
                );
            })
        .RequireAuthorization("Admin")
        .WithName("InsertServices")
        .WithOpenApi();

        app.MapPut("/{id}",
            async (
                int id,
                ServiceDTO request,
                IValidator<ServiceDTO> validator,
                IServiceService service
            ) =>
            {
                var requestValidator = await validator.ValidateAsync(request);

                if (!requestValidator.IsValid)
                    return Results.ValidationProblem(requestValidator.ToDictionary());

                var response = await service.UpdateServiceAsync(id,request);

                return response.Match(
                    left => Results.Problem(left.Message, statusCode: (int)HttpStatusCode.InternalServerError),
                    right => Results.Ok(ApiResponse<ServiceDTO>.Ok(right))
                );
            })
        .RequireAuthorization("Admin")
        .WithName("UpdateServices")
        .WithOpenApi();

        app.MapDelete("/{id}",
           async (
               int id,
               IServiceService service
           ) =>
           {
               await service.DeleteServiceAsync(id);
               return Results.NoContent();
           })
       .RequireAuthorization("Admin")
       .WithName("DeleteServices")
       .WithOpenApi();
    }
}
