using FluentValidation;
using FluentValidation.AspNetCore;
using Icon3DPack.API.Application;
using Icon3DPack.API.Application.Models.Validators;
using Icon3DPack.API.DataAccess;
using Icon3DPack.API.DataAccess.Persistence;
using Icon3DPack.API.Host;
using Icon3DPack.API.Host.Filters;
using Icon3DPack.API.Host.Middleware;
using Icon3DPack.API.AwsS3;
using Icon3DPack.API.AwsS3.Models.AwsS3;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers(
    config => config.Filters.Add(typeof(ValidateModelAttribute))
);
builder.Services.Configure<AwsS3Configuration>(builder.Configuration.GetSection("AwsS3Configuration"));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(IValidationsMarker));

builder.Services.AddSwagger();

builder.Services.AddDataAccess(builder.Configuration)
    .AddApplication(builder.Environment);

// Add JWT configuration
builder.Services.AddJwt(builder.Configuration);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});

builder.Services.AddEmailConfiguration(builder.Configuration);
builder.Services.AddAwsS3Configuration(builder.Configuration);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

using var scope = app.Services.CreateScope();

await AutomatedMigration.MigrateAsync(scope.ServiceProvider);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(corsPolicyBuilder =>
    corsPolicyBuilder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<PerformanceMiddleware>();

app.UseMiddleware<TransactionMiddleware>();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();

namespace Icon3DPack.API.Host
{
    public partial class Program { }
}
