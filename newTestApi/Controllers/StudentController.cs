using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using newTestApi.Data;
using newTestApi.Models.Domain;
using newTestApi.Models.DTO;

namespace newTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentDbContext dBContext;
        public StudentController(StudentDbContext dBContext)
        {
            this.dBContext = dBContext;
        }

        //get all
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ///get data from database-domain model
            var studentDomain=await dBContext.Students.ToListAsync();
            //mapping domain model to Dto
            var studentDto = new List<StudentDto>();
            foreach (var student in studentDomain)
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

        //get by id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //getting from domain model
            var studentDomain=await dBContext.Students.FirstOrDefaultAsync(x=>x.Id==id);

            if (studentDomain == null)
            {
                return NotFound();
            }

            //mapping from domain model to dto
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
        public async Task<IActionResult> Create([FromBody]AddStudentRequestDto addStudentRequestDto)
        {
            //Map or convert DTO to domain model
            var studentDomainModel = new Student
            {
                RegNo = addStudentRequestDto.RegNo,
                Name=addStudentRequestDto.Name,
                Address=addStudentRequestDto.Address,
                Remarks=addStudentRequestDto.Remarks,
                MarksId = addStudentRequestDto.MarksId
            };
            //using domain model to create student record
            await dBContext.Students.AddAsync(studentDomainModel);
            await dBContext.SaveChangesAsync();
            //mapping domain model to dto
            var studentDto = new StudentDto
            {
                Id = studentDomainModel.Id,
                RegNo = studentDomainModel.RegNo,
                Name = studentDomainModel.Name,
                Address = studentDomainModel.Address,
                Remarks = studentDomainModel.Remarks,
                MarksId=studentDomainModel.MarksId
            };
            return CreatedAtAction(nameof(GetById), new {id=studentDto.Id},studentDto);
        }

        //Update student record
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateStudentRequestDto updateStudentRequestDto)
        {
            var studentDomainModel=await dBContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (studentDomainModel == null)
            {
                return NotFound();
            }

            //map dto to domain model
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

        //Delete student record
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var studentDomainModel=await dBContext.Students.FirstOrDefaultAsync(x=>x.Id==id);
            if(studentDomainModel == null)
            {
                return NotFound();
            }
            //delete student
            dBContext.Students.Remove(studentDomainModel);
            await dBContext.SaveChangesAsync();
            
            return Ok();
        }
    }
}
