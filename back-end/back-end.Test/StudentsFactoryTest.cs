using students_db.Repository;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace students_db.Tests;

public class StudentsFactoryTest : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var root = new InMemoryDatabaseRoot();

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<StudentsContext>));

            services.AddDbContext<StudentsContext>(options =>
                options.UseInMemoryDatabase("StudentsDatabase", root));
        });

        return base.CreateHost(builder);
    }
}