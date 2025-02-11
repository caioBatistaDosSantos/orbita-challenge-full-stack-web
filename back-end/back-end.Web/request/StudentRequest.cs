using System.ComponentModel.DataAnnotations;

namespace students_db.Request;
public class StudentRequest
{
    [Required(ErrorMessage="Required field")]
    [MinLength(3, ErrorMessage="Name must be at least 3 characters long")]
    public string Name { get; set; }
    [Required(ErrorMessage="Required field")]
    [RegularExpression(".+\\@.+\\..+",ErrorMessage = "Email must be in a valid format")]
    public string Email { get; set; }

}