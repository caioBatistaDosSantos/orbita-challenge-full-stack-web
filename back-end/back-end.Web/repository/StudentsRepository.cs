using Microsoft.EntityFrameworkCore;
using students_db.Models;
using students_db.Request;
using System.Threading;

namespace students_db.Repository;

public class StudentsRepository
{
    protected readonly StudentsContext _context;
    public StudentsRepository(StudentsContext context)
    {
        _context = context;
    }

    public async Task<List<Student>> GetAll()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<Student> GetByPK(int ra)
    {
        return await _context.Students.FindAsync(ra);
    }

    public Student Create(Student student)
    {
        _context.Add(student);
        _context.SaveChanges();
        return student;
    }

    public async virtual void Update(int ra, StudentRequest request)
    {
        var student = await GetByPK(ra);

        student.Name = request.Name;
        student.Email = request.Email;

        _context.SaveChanges();
    }

    public virtual void Delete(int ra)
    {
        var student = _context.Students.Where(e => e.RA == ra).First();
        _context.Remove(student);
        _context.SaveChanges();
    }
}