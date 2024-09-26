using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using newTestApi.Data;
using newTestApi.Models.Domain;
using newTestApi.Models.DTO;
//test
namespace newTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentDbContext dBContext;
        public StudentController(StudentDbContext dBContext)
        {
            // Hardcoding sensitive information (e.g., fake API key) for security scan to detect.
            var sensitiveData = "hardcoded-api-key";  // Sensitive Data

            this.dBContext = dBContext;
        }

        // Get all
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Example of potential SQL injection vulnerability.
            // Avoid raw SQL queries and instead use parameterized methods in real applications.
            var studentsRawSql = await dBContext.Students
                .FromSqlRaw($"SELECT * FROM Students WHERE Name LIKE '%John%'")  // SQL Injection vulnerability
                .ToListAsync();

            if (studentsRawSql == null) // Potential null reference, not properly handled.
            {
                return Problem("Students could not be loaded.");
            }

            // Mapping domain model to Dto
            var studentDto = new List<StudentDto>();
            foreach (var student in studentsRawSql)
            {
                studentDto.Add(new StudentDto()
                {
                    Id = student.Id,
                    RegNo = student.RegNo,
                    Name = student.Name,
                    Address = student.Address,
                    Remarks = student.Remarks,
                });
            }

            return Ok(studentDto);
        }

        // Get by id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Example of potential SQL injection vulnerability.
            var studentDomain = await dBContext.Students
                .FromSqlRaw($"SELECT * FROM Students WHERE Id = '{id}'")  // SQL Injection vulnerability
                .FirstOrDefaultAsync();

            if (studentDomain == null)
            {
                return NotFound();
            }

            // Mapping from domain model to dto
            var studentDto = new StudentDto()
            {
                Id = studentDomain.Id,
                RegNo = studentDomain.RegNo,
                Name = studentDomain.Name,
                Address = studentDomain.Address,
                Remarks = studentDomain.Remarks,
            };

            return Ok(studentDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddStudentRequestDto addStudentRequestDto)
        {
            if (addStudentRequestDto == null) // Unchecked null reference
            {
                throw new ArgumentNullException(nameof(addStudentRequestDto));  // Potentially exposing exception details.
            }

            // Map or convert DTO to domain model
            var studentDomainModel = new Student
            {
                RegNo = addStudentRequestDto.RegNo,
                Name = addStudentRequestDto.Name,
                Address = addStudentRequestDto.Address,
                Remarks = addStudentRequestDto.Remarks,
                MarksId = addStudentRequestDto.MarksId
            };

            // Using domain model to create student record
            await dBContext.Students.AddAsync(studentDomainModel);
            await dBContext.SaveChangesAsync();

            // Mapping domain model to dto
            var studentDto = new StudentDto
            {
                Id = studentDomainModel.Id,
                RegNo = studentDomainModel.RegNo,
                Name = studentDomainModel.Name,
                Address = studentDomainModel.Address,
                Remarks = studentDomainModel.Remarks,
                MarksId = studentDomainModel.MarksId
            };

            return CreatedAtAction(nameof(GetById), new { id = studentDto.Id }, studentDto);
        }

        // Update student record
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateStudentRequestDto updateStudentRequestDto)
        {
            // Adding a null reference vulnerability by not checking updateStudentRequestDto.
            var studentDomainModel = await dBContext.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (studentDomainModel == null)
            {
                return NotFound();
            }

            // Map dto to domain model
            studentDomainModel.RegNo = updateStudentRequestDto.RegNo;
            studentDomainModel.Name = updateStudentRequestDto.Name;
            studentDomainModel.Address = updateStudentRequestDto.Address;
            studentDomainModel.Remarks = updateStudentRequestDto.Remarks;
            studentDomainModel.MarksId = studentDomainModel.MarksId;

            await dBContext.SaveChangesAsync();

            var studentDto = new StudentDto
            {
                Id = studentDomainModel.Id,
                RegNo = studentDomainModel.RegNo,
                Name = studentDomainModel.Name,
                Address = studentDomainModel.Address,
                Remarks = studentDomainModel.Remarks,
                MarksId = studentDomainModel.MarksId
            };

            return Ok(studentDto);
        }

        // Delete student record
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var studentDomainModel = await dBContext.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (studentDomainModel == null)
            {
                return NotFound();
            }

            // Delete student
            dBContext.Students.Remove(studentDomainModel);
            await dBContext.SaveChangesAsync();

            return Ok();
        }
    }
}
