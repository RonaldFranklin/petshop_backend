using Carter;
using FluentValidation;
using PetShop.Modules.Pets.Interfaces;
using PetShop.Response;
using System.Net;
using System.Security.Claims;

namespace PetShop.Modules.Pets;

public class PetEndpoints : CarterModule
{
    public PetEndpoints() : base("pets") { }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/",
           async (
               IPetService service,
               ClaimsPrincipal user
           ) =>
           {
               int.TryParse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);
               string? userRole = user?.FindFirst(ClaimTypes.Role)?.Value;
               int ownerId = userRole == "manager" ? 0 : userId;

               var response = await service.GetPetsAsync(ownerId);

               return response.Match(
                   left => Results.Problem(left.Message, statusCode: (int)HttpStatusCode.InternalServerError),
                   right => Results.Ok(ApiResponse<List<PetDTO>>.Ok(right))
               );
           })
       .RequireAuthorization()
       .WithName("GetPets")
       .WithOpenApi();

        app.MapGet("/{id}",
           async (
               int id,
               IPetService service,
               ClaimsPrincipal user
           ) =>
           {
               int.TryParse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);
               string? userRole = user?.FindFirst(ClaimTypes.Role)?.Value;
               int ownerId = userRole == "manager" ? 0 : userId;

               var response = await service.GetPetByIdAsync(id, ownerId);

               return response.Match(
                   left => Results.Problem(left.Message, statusCode: (int)HttpStatusCode.InternalServerError),
                   right => Results.Ok(ApiResponse<PetDTO>.Ok(right))
               );
           })
       .RequireAuthorization()
       .WithName("GetPetsById")
       .WithOpenApi();

        app.MapPost("/",
            async (
                PetDTO request,
                IValidator<PetDTO> validator,
                IPetService service,
                ClaimsPrincipal user
            ) =>
            {
                var requestValidator = await validator.ValidateAsync(request);

                if (!requestValidator.IsValid)
                    return Results.ValidationProblem(requestValidator.ToDictionary());

                int.TryParse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);
                
                request.OwnerId = userId;

                var response = await service.InsertPetAsync(request);

                return response.Match(
                    left => Results.Problem(left.Message, statusCode: (int)HttpStatusCode.InternalServerError),
                    right => Results.Ok(ApiResponse<PetDTO>.Ok(right))
                );
            })
        .RequireAuthorization("Client")
        .WithName("InsertPets")
        .WithOpenApi();

        app.MapPut("/{id}",
            async (
                int id,
                PetDTO request,
                IValidator<PetDTO> validator,
                IPetService service,
                ClaimsPrincipal user
            ) =>
            {
                var requestValidator = await validator.ValidateAsync(request);

                if (!requestValidator.IsValid)
                    return Results.ValidationProblem(requestValidator.ToDictionary());

                int.TryParse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);
                int ownerId = userId;

                var response = await service.UpdatePetAsync(id, request, userId);

                return response.Match(
                    left => Results.Problem(left.Message, statusCode: (int)HttpStatusCode.InternalServerError),
                    right => Results.Ok(ApiResponse<PetDTO>.Ok(right))
                );
            })
        .RequireAuthorization("Client")
        .WithName("UpdatePets")
        .WithOpenApi();

        app.MapDelete("/{id}",
           async (
               int id,
               IPetService service,
               ClaimsPrincipal user
           ) =>
           {
               int.TryParse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);
               int ownerId = userId;

               await service.DeletePetAsync(id, ownerId);
               return Results.NoContent();
           })
       .RequireAuthorization("Client")
       .WithName("DeletePets")
       .WithOpenApi();
    }
}
