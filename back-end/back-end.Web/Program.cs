using students_db.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<StudentsContext>(options =>
{
    string connectionString = @"Server=db; Database=students_db; Uid=root; Pwd=123456;";
    options.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions =>
            mySqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null)
        );
});

builder.Services.AddScoped<StudentsRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

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
}

app.UseHttpsRedirection();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program {}