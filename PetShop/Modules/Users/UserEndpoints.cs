using FluentValidation;
using PetShop.Modules.Users.Interfaces;
using PetShop.Modules.Users;
using System.Net;
using PetShop.Response;
using Carter;
using System.Security.Claims;

namespace UserShop.Modules.Users;

public class UserEndpoints : CarterModule
{
    public UserEndpoints() : base("users") { }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/",
           async (
               IUserService service,
               ClaimsPrincipal user
           ) =>
           {
               int.TryParse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);
               string? userRole = user?.FindFirst(ClaimTypes.Role)?.Value;
               int userToSearchId = userRole == "manager" ? 0 : userId;

               var response = await service.GetUsersAsync(userToSearchId);

               return response.Match(
                   left => Results.Problem(left.Message, statusCode: (int)HttpStatusCode.InternalServerError),
                   right => Results.Ok(ApiResponse<List<UserDTO>>.Ok(right))
               );
           })
       .RequireAuthorization()
       .WithName("GetUsers")
       .WithOpenApi();

        app.MapGet("/{id}",
           async (
               int id,
               IUserService service,
               ClaimsPrincipal user
           ) =>
           {
               int.TryParse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);
               string? userRole = user?.FindFirst(ClaimTypes.Role)?.Value;
               int userToSearchId = userRole == "manager" ? id : userId;

               var response = await service.GetUserByIdAsync(userToSearchId);

               return response.Match(
                   left => Results.Problem(left.Message, statusCode: (int)HttpStatusCode.InternalServerError),
                   right => Results.Ok(ApiResponse<UserDTO>.Ok(right))
               );
           })
       .RequireAuthorization()
       .WithName("GetUsersById")
       .WithOpenApi();

        app.MapPost("/",
            async (
                UserDTO request,
                IValidator<UserDTO> validator,
                IUserService service
            ) =>
            {
                var requestValidator = await validator.ValidateAsync(request);

                if (!requestValidator.IsValid)
                    return Results.ValidationProblem(requestValidator.ToDictionary());

                var response = await service.InsertUserAsync(request);

                return response.Match(
                    left => Results.Problem(left.Message, statusCode: (int)HttpStatusCode.InternalServerError),
                    right => Results.Ok(ApiResponse<UserDTO>.Ok(right))
                );
            })
        .WithName("InsertUsers")
        .WithOpenApi();

        app.MapPut("/{id}",
            async (
                int id,
                UserDTO request,
                IValidator<UserDTO> validator,
                IUserService service,
                ClaimsPrincipal user
            ) =>
            {
                var requestValidator = await validator.ValidateAsync(request);

                if (!requestValidator.IsValid)
                    return Results.ValidationProblem(requestValidator.ToDictionary());

                bool hasId = int.TryParse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);

                var response = await service.UpdateUserAsync(hasId ? userId : id, request);

                return response.Match(
                    left => Results.Problem(left.Message, statusCode: (int)HttpStatusCode.InternalServerError),
                    right => Results.Ok(ApiResponse<UserDTO>.Ok(right))
                );
            })
        .RequireAuthorization()
        .WithName("UpdateUsers")
        .WithOpenApi();

        app.MapDelete("/{id}",
           async (
               int id,
               IUserService service,
               ClaimsPrincipal user
           ) =>
           {
               int.TryParse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);
               string? userRole = user?.FindFirst(ClaimTypes.Role)?.Value;
               int userToDeleteId = userRole == "manager" ? id : userId;

               await service.DeleteUserAsync(userToDeleteId);
               return Results.NoContent();
           })
       .RequireAuthorization()
       .WithName("DeleteUsers")
       .WithOpenApi();
    }
}
