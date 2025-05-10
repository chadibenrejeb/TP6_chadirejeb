using Microsoft.EntityFrameworkCore;
using SchoolAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SchoolDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("SchoolConnection")));


// Ajoutez AutoMapper
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddScoped<IUniversityRepository, SchoolRepository>();


// Add services to the container.

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
