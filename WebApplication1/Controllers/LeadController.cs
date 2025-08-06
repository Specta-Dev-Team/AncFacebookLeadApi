using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Mapper;
using WebApplication1.Model;
using WebApplication1.Wrapper;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class LeadController : Controller
    {
        public AppDbContext _appDbContext;


        public LeadController(AppDbContext appDbContext) {

            _appDbContext = appDbContext;
        }


        [HttpPost]
        public async Task<IActionResult> PostLeads([FromBody] LeadDto dto)
        {

            

            if (dto == null)
            {
                return BadRequest("No Lead Available");
            }


            var model = LeadMapper.ToModel(dto);

            var sql = @"

            INSERT INTO ANCNEW.LEADS1 (
            leadgenId, formId,dateCreated,universityName,location,firstName,secondName
             ,email,fullName,phoneNumber
            )
            VALUES (
             :leadgenId,:formId,:dateCreated,:universityName,:location,:firstName,:secondName
             ,:email,:fullName,:phoneNumber
            )";

            var parameters = new[]
             {
  
    new OracleParameter("LEADGENID", dto.leadgenId),
    new OracleParameter("FORMID", dto.formId),
    
    new OracleParameter("DATECREATED",dto.dateCreated),
    new OracleParameter("UNIVERSITYNAME",dto.data.university_name),
    new OracleParameter("LOCATION",dto.data.location),
    new OracleParameter("FIRSTNAME",dto.data.first_name),
    new OracleParameter("SECONDNAME",dto.data.second_name),
    new OracleParameter("EMAIL",dto.data.email),
    new OracleParameter("FULLNAME",dto.data.full_name),
    new OracleParameter("PHONENUMBER",dto.data.phone_number)
            };


            await _appDbContext.Database.ExecuteSqlRawAsync(sql, parameters);

            return Ok(new {status="sucess" ,received=dto});
        }
    }
}
