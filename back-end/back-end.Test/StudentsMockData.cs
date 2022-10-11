using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using students_db.Models;
using students_db.Repository;

namespace students_db.Tests;

public class StudentsMockData
{
    public static async Task CreateAPI(StudentsFactoryTest application, bool create)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            using (var studentDbContext = provider.GetRequiredService<StudentsContext>())
            {
                await studentDbContext.Database.EnsureCreatedAsync();

                if (create)
                {
                    await studentDbContext.Students.AddAsync(new Student
                    {
                        RA = 1111,
                        Name = "Students 1",
                        Email = "student1@email.com",
                        CPF = 11111111111,
                    });
                    
                    await studentDbContext.Students.AddAsync(new Student 
                    {
                        RA = 2222,
                        Name = "Students 2",
                        Email = "student2@email.com",
                        CPF = 22222222222,
                    });
                    
                    await studentDbContext.SaveChangesAsync();
                }
            }
        }
    }
}