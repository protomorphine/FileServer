using FileServer.Core.Models;
using FileServer.API.Models;
using FileServer.Core.Services;
using FileServer.API.Models.Exceptions;
using System.Data.Entity.Core;
using Microsoft.EntityFrameworkCore;
using Hellang.Middleware.ProblemDetails;
using FileServer.Core.Repositories;
using FileServer.Core.Services.Interfaces;
using FileServer.Infrastructure.Data;
using FileServer.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json").Build();
var dbOptions = builder.Configuration.GetSection("DbOptions").Get<DbOptions>();
var storageOptions = builder.Configuration.GetSection("StorageOptions").Get<StorageOptions>();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<DbOptions>(dbOptions);
builder.Services.AddSingleton<StorageOptions>(storageOptions);

builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IFileRepository, FileRepository>();

builder.Services.AddProblemDetails(options => {
    options.Map<FileNotFoundException>(ex => new ExtendedExceptionProblemDetails(ex, StatusCodes.Status404NotFound));
    options.Map<ObjectNotFoundException>(ex => new ExtendedExceptionProblemDetails(ex, StatusCodes.Status404NotFound));
});

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlite(dbOptions.ConnectionString);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseProblemDetails();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
