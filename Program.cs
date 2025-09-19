using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using remote_poc_webapi.Controllers;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<MyDbContext>(option =>
            option.UseSqlServer("Server=tcp:poc-scopx.database.windows.net,1433;Initial Catalog=poc-remote;Persist Security Info=False;User ID=remote-admin;Password=lastresort61$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseStaticFiles();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
    RequestPath = "/StaticFiles",
    EnableDefaultFiles = true
});

app.UseFileServer(new FileServerOptions
{

    FileProvider = new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "vip")),
    RequestPath = "/vip",
    EnableDefaultFiles = true
    });

app.UseFileServer(new FileServerOptions
{

    FileProvider = new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "pqc")),
    RequestPath = "/pqc",
    EnableDefaultFiles = true
});

//app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    //Console.WriteLine($"Incoming request Content-Type: {context.Request.ContentType}");

    if (context.Request.ContentType == "application/octet-stream")
    {
        context.Request.EnableBuffering(); // Enable reading binary data
    }
    await next();
});

app.MapControllers();

app.Run();
