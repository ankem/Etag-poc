using Microsoft.EntityFrameworkCore;
using EtagWebAPI.Models;
using EtagWebAPI.Filters;
using EtagWebAPI.Utils;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(typeof(ETagFilter));
    opt.Filters.Add(typeof(EtagResponseFilter));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// build database context
builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDbContext<EtagContext>(opt => opt.UseInMemoryDatabase("EtagList"));
builder.Services.AddScoped<IEtagService, EtagService>();
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
