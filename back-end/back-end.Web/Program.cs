using students_db.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddScoped<StudentsRepository>();
builder.Services.AddControllers();
builder.Services.AddDbContext<StudentsContext>(options => 
{
    options.UseMySql(
                @"Server=127.0.0.1; Database=students_db; Uid=root; Pwd=123456;",
                new MySqlServerVersion(new Version(8, 0, 11))
                // new MySqlServerVersion(new Version(8, 0, 11))
                // Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql")
            );
});
builder.Services.AddScoped<StudentsRepository>();
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

using(var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<StudentsContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();  
    // use context
}

// StudentsContext context = new();

// context.Database.EnsureDeleted();
// context.Database.EnsureCreated();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program {}