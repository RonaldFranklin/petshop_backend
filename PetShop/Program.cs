using Carter;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetShop;
using PetShop.Data;
using PetShop.Modules.Pets;
using PetShop.Modules.Pets.Interfaces;
using PetShop.Modules.Pets.Validator;
using PetShop.Modules.Scheduling;
using PetShop.Modules.Scheduling.Interfaces;
using PetShop.Modules.Scheduling.Validator;
using PetShop.Modules.Services;
using PetShop.Modules.Services.Interfaces;
using PetShop.Modules.Services.Validator;
using PetShop.Modules.Users;
using PetShop.Modules.Users.Interfaces;
using PetShop.Modules.Users.Validator;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var key = Encoding.ASCII.GetBytes(Settings.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("Admin", policy => policy.RequireRole("manager"));
    x.AddPolicy("Client", policy => policy.RequireRole("client"));
});

builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddValidatorsFromAssemblyContaining<ServiceDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PetDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserLoginDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SchedulingDTOValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();

builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<ISchedulingRepository, SchedulingRepository>();

builder.Services.AddTransient<IServiceService, ServiceService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPetService, PetService>();
builder.Services.AddTransient<ISchedulingService, SchedulingService>();

var app = builder.Build();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapCarter();

app.MapPost("/login", async (UserLoginDTO login, IValidator<UserLoginDTO> validator, IUserService service) =>
{
    var requestValidator = await validator.ValidateAsync(login);

    if (!requestValidator.IsValid)
        return Results.ValidationProblem(requestValidator.ToDictionary());

    var response = await service.Login(login.Email, login.Password);

    return response.Match(
        left => Results.Problem(left.Message, statusCode: (int)HttpStatusCode.InternalServerError),
        right => Results.Ok(new
        {
            StatusCode = 200,
            Success = true,
            Data = new
            { 
                Token = right
            }
        })
    );
})
.WithName("Login")
.WithOpenApi(); ;

app.Run();
