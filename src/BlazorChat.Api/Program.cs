using AutoMapper;
using AutoMapper.EquivalencyExpression;
using BlazorChat.Api;
using BlazorChat.Api.Chatting;
using BlazorChat.Api.Database;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;

services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => 
        policy.WithOrigins(new[] { "http://localhost:5269" })
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseInMemoryDatabase("ApplicationDatabase");
        return;
    }

    string? connectionString = builder.Configuration
        .GetConnectionString(
            ConfigurationBlockNames.DefaultConnectionString);
            
    options.UseSqlServer(connectionString);
});

services.AddAutoMapper((serviceProvider, automapper) =>
{
    automapper.AddCollectionMappers();
    automapper.UseEntityFrameworkCoreModel<ApplicationDbContext>(serviceProvider);
}, typeof(AppMappingProfile).Assembly);

services.AddSignalR();
services.AddResponseCompression(options =>
{
    options.MimeTypes =
        ResponseCompressionDefaults.MimeTypes
            .Concat(new[] { "application/ictet-stream" });
});

services.AddEndpointsApiExplorer();

services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseResponseCompression();

#if !DEBUG
app.UseHttpsRedirection();
#endif

app.MapHub<ChatHub>("/ChatHub");

app.UseCors();

app.Run();
