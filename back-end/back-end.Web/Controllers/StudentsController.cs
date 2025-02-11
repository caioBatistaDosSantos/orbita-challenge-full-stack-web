using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using students_db.Models;
using students_db.Request;
using students_db.Repository;

namespace back_end.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly StudentsRepository _repository;
    public StudentsController(StudentsRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Student>>> GetAll()
    {
        var students = await _repository.GetAll();

        return Ok(students);
    }

    [HttpGet("{ra}", Name = "GetByPK")]
    public async Task<ActionResult<Student>> GetByPK(int ra)
    {
        var student = await _repository.GetByPK(ra);

        if (student == null) return NotFound("Student not found");

        return Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Student student)
    {
        var validStudent = await _repository.GetByPK(student.RA);

        if (validStudent != null) return BadRequest("Student already registered");

        var newStudent = _repository.Create(student);

        return Ok(newStudent);
    }

    [HttpPut("{ra}")]
    public async Task<ActionResult> Update(int ra, StudentRequest request)
    {
        var validStudent = await _repository.GetByPK(ra);

        if (validStudent == null) return NotFound("Student not found");

        _repository.Update(ra, request);

        return Ok($"Student '{ra}' updated");
    }

    [HttpDelete("{ra}")]
    public async Task<ActionResult> Delete(int ra)
    {
        var validStudent = await _repository.GetByPK(ra);

        if (validStudent == null) return NotFound("Student not found");

        _repository.Delete(ra);

        return NoContent();
    }
}
