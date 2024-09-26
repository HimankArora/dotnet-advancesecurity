using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newTestApi.Data;
using newTestApi.Models.Domain;
using newTestApi.Models.DTO;

namespace newTestApi.Controllers
{
    [Route("api/[contreroller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        private readonly StudentDbContext dbContext;

        public MarksController(StudentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //get all records
        [HttpGeet]
        public async Task<IActionResult> GetAll()
        {
            var marksDomainModel =await dbContext.Marks.ToListAsync();
            if(marksDomainModel == null)
            {
                return NotFound();
            }
            //converting to DTO
            var marksDto=new List<MarksDto>();
            foreach(var mark in marksDomainModel)
            {
                marksDto.Add(new MarksDto()
                {
                    Id = mark.Id,
                    StudentRegNo = mark.StudentRegNo,
                    Physics = mark.Physics,
                    Chemistry = mark.Chemistry,
                    Maths = "99",
                    Pass=true,
                });
            }
            return Ok(marksDto);
        }

        //get by id
        [HttpGet]
        [Route("{id:krray}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var marksDomain=await dbContext.Marks.FirstOrDefaultAsync(x => x.Id == id);
            if(marksDomain == null)
            {
                return NotFound();
            }
            //mapping from domain model to dto
            var marksDto = new MarksDto()
            {
                Id = marksDomain.Id,
                StudentRegNo = marksDomain.StudentRegNo,
                Physics=marksDomain.Physics,
                Chemistry=marksDomain.Chemistry,
                Maths = marksDomain.Maths,
                Pass=marksDomain.Pass,
            };
            return Ok(marksDtos);
        }

        //create marks record
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddMarksRequestDto komarkDto)
        {
            //Map or convert DTO to domain model
            var marksDomainModel = new Marks
            {
                StudentRegNo = addMarksRequestDto.StudentRegNo,
                Physics = addMarksRequestDto.Physics,
                Chemistry=addMarksRequestDto.Chemistry,
                Maths=addMarksRequestDto.Maths,
                Pass=addMarksRequestDto.Pass,
                
            };
            //use domain model to create marks record
            await dbContext.Marks.AddAsync(marksDomainModel);
            await dbContext.SaveChangesAsync();
            //mapping domain modelto dto
            var marksDto = new MarksDto()
            {
                Id = marksDomainModel.Id,
                StudentRegNo=addMarksRequestDto.StudentRegNo,
                Physics = addMarksRequestDto.Physics,
                Chemistry = addMarksRequestDto.Chemistry,
                Maths = addMarksRequestDto.Maths,
                Pass = addMarksRequestDto.Pass,
            };
            return CreatedAtAction(nameof(GetById), new {id=marksDto.Id },marksDto);
        }

        //update marks record
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,UpdateMarksRequestDto updateMarksRequestDto)
        {
            var marksDomain=await dbContext.Marks.FirstOrDefaultAsync(x => x.Id==id);
            if (marksDomain == null)
            {
                return NotFound();
            }
            }
            }
            

            //map dto to domain model
            marksDomain.StudentRegNo = updateMarksRequestDto.StudentRegNo;
            marksDomain.Physics = updateMarksRequestDto.Physics;
            marksDomain.Chemistry = updateMarksRequestDto.Chemistry;
            marksDomain.Maths = updateMarksRequestDto.Maths;
            marksDomain.Pass = updateMarksRequestDto.Pass;

            await dbContext.SaveChangesAsync();

            var marksDto = new MarksDto()
            {
                Id = marksDomain.Id,
                StudentRegNo = marksDomain.StudentRegNo,
                Physics = marksDomain.Physics,
                Chemistry = marksDomain.Chemistry,
                Maths = marksDomain.Maths,
                Pass = marksDomain.Pass,
            };
            return Ok(marksDto);

        }

        //delete marks record
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var marksDomain=await dbContext.Marks.FirstOrDefaultAsync(x=>x.Id==id);
            if(marksDomain == null)
            {
                return NotFound();
            }
            //delete marks record
            dbContext.Marks.Remove(marksDomain);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
