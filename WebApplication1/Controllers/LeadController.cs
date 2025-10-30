using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using WebApplication1.Data;
using WebApplication1.Dto;
using WebApplication1.Mapper;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class LeadController : Controller
    {
        public AppDbContext _appDbContext;


        public LeadController(AppDbContext appDbContext)
        {

            _appDbContext = appDbContext;
        }

        //Api For Facebook Lead Data
        [HttpPost]
        public async Task<IActionResult> PostLeads([FromForm] LeadDto dto)
        {



            if (dto == null)
            {
                return BadRequest("No Lead Available");
            }


            var model = LeadMapper.ToModel(dto);



            var sql = @"
INSERT INTO ANCNEW.CHAT_LEADS(
     DOCID, DOCDATE, FNAME, LNAME, CONTACT, EMAIL, LEADSOURCE, SLEADSOURCE,
    LEADTYPE, SLEADTYPE, COUNSELLOR, PRE_PROLEVEL, BNAME, SCHOOL, TELEPHONE, DOB,
    SALUTATION, MNAME, P_TYPE, ADDR1, ADDR2, ADDR3, DISTRICT, GENDER, OL_YEAR,
    OL_TYPE, AL_YEAR, AL_TYPE, PREF_DESTINATION, PREF_FIELD, PREF_FIELD_OTHER,
    DEGREE, PROF_QUAL, PROF_QUAL_OTHER, OTHER_QUAL, EMP_COMPANY, DESIGNATION,
    WORK_EXP, SUBLEAD_OTHER, FATHER_NAME, FATHER_OCCUP, FATHER_MOBILE, FATHER_EMAIL,
    MOTHER_NAME, MOTHER_OCCUP, MOTHER_MOBILE, MOTHER_EMAIL, GUARD_OCCUP,
    GUARD_MOBILE, TOT_FUND, MON_FUND, GUARD_NAME, GUARD_EMAIL, COUNTRY, PROVINCE,
    LEADST, DOCNAME, TRANSFERDATE, TRANSFERSTATUS,REMARKS
)
VALUES(
     :DOCID, :DOCDATE, :FNAME, :LNAME, :CONTACT, :EMAIL, :LEADSOURCE, :SLEADSOURCE,
    :LEADTYPE, :SLEADTYPE, :COUNSELLOR, :PRE_PROLEVEL, :BNAME, :SCHOOL, :TELEPHONE, :DOB,
    :SALUTATION, :MNAME, :P_TYPE, :ADDR1, :ADDR2, :ADDR3, :DISTRICT, :GENDER, :OL_YEAR,
    :OL_TYPE, :AL_YEAR, :AL_TYPE, :PREF_DESTINATION, :PREF_FIELD, :PREF_FIELD_OTHER,
    :DEGREE, :PROF_QUAL, :PROF_QUAL_OTHER, :OTHER_QUAL, :EMP_COMPANY, :DESIGNATION,
    :WORK_EXP, :SUBLEAD_OTHER, :FATHER_NAME, :FATHER_OCCUP, :FATHER_MOBILE, :FATHER_EMAIL,
    :MOTHER_NAME, :MOTHER_OCCUP, :MOTHER_MOBILE, :MOTHER_EMAIL, :GUARD_OCCUP,
    :GUARD_MOBILE, :TOT_FUND, :MON_FUND, :GUARD_NAME, :GUARD_EMAIL, :COUNTRY, :PROVINCE,
    :LEADST, :DOCNAME, :TRANSFERDATE, :TRANSFERSTATUS,:REMARKS
)";

            var parameters = new[]
             {

    new OracleParameter("DOCID", GetDbValue(dto.data.DocumentId)),
    new OracleParameter("DOCDATE", TryDate(dto.data.DocumentDate)),
    new OracleParameter("FNAME", GetDbValue(dto.data.FirstName)),
    new OracleParameter("LNAME", GetDbValue(dto.data.LastName)),
    new OracleParameter("CONTACT", GetDbValue(dto.data.ContactNumber)),
    new OracleParameter("EMAIL", GetDbValue(dto.data.Email)),
    new OracleParameter("LEADSOURCE", GetDbValue(dto.data.LeadSource)),
    new OracleParameter("SLEADSOURCE", GetDbValue(dto.data.SubLeadSource)),
    new OracleParameter("LEADTYPE", GetDbValue(dto.data.LeadType)),
    new OracleParameter("SLEADTYPE", GetDbValue(dto.data.SubLeadType)),
    new OracleParameter("COUNSELLOR", GetDbValue(dto.data.Counsellor)),
    new OracleParameter("PRE_PROLEVEL", GetDbValue1(dto.data.PreviousProgramLevel)),
    new OracleParameter("BNAME", GetDbValue(dto.data.BranchName)),
    new OracleParameter("SCHOOL", GetDbValue(dto.data.SchoolName)),
    new OracleParameter("TELEPHONE", GetDbValue(dto.data.Telephone)),
    new OracleParameter("DOB", TryDate(dto.data.DateOfBirth)),
    new OracleParameter("SALUTATION", GetDbValue(dto.data.Salutation)),
    new OracleParameter("MNAME", GetDbValue(dto.data.MiddleName)),
    new OracleParameter("P_TYPE", GetDbValue1(dto.data.ProgramType)),
    new OracleParameter("ADDR1", GetDbValue(dto.data.AddressLine1)),
    new OracleParameter("ADDR2", GetDbValue(dto.data.AddressLine2)),
    new OracleParameter("ADDR3", GetDbValue(dto.data.AddressLine3)),
    new OracleParameter("DISTRICT", GetDbValue(dto.data.District)),
    new OracleParameter("GENDER", GetDbValue(dto.data.Gender)),
    new OracleParameter("OL_YEAR", GetDbValue(dto.data.OLevelYear)),
    new OracleParameter("OL_TYPE", GetDbValue(dto.data.OLevelType)),
    new OracleParameter("AL_YEAR", GetDbValue(dto.data.ALevelYear)),
    new OracleParameter("AL_TYPE", GetDbValue(dto.data.ALevelType)),
    new OracleParameter("PREF_DESTINATION", GetDbValue1(dto.data.PreferredDestination)),
    new OracleParameter("PREF_FIELD", GetDbValue(dto.data.PreferredField)),
    new OracleParameter("PREF_FIELD_OTHER", GetDbValue(dto.data.PreferredFieldOther)),
    new OracleParameter("DEGREE", GetDbValue(dto.data.Degree)),
    new OracleParameter("PROF_QUAL", GetDbValue(dto.data.ProfessionalQualification)),
    new OracleParameter("PROF_QUAL_OTHER", GetDbValue(dto.data.ProfessionalQualificationOther)),
    new OracleParameter("OTHER_QUAL", GetDbValue(dto.data.OtherQualification)),
    new OracleParameter("EMP_COMPANY", GetDbValue(dto.data.EmploymentCompany)),
    new OracleParameter("DESIGNATION", GetDbValue(dto.data.Designation)),
    new OracleParameter("WORK_EXP", TryInt(dto.data.WorkExperience)),
    new OracleParameter("SUBLEAD_OTHER", GetDbValue(dto.data.SubLeadOther)),
    new OracleParameter("FATHER_NAME", GetDbValue(dto.data.FatherName)),
    new OracleParameter("FATHER_OCCUP", GetDbValue(dto.data.FatherOccupation)),
    new OracleParameter("FATHER_MOBILE", GetDbValue(dto.data.FatherMobile)),
    new OracleParameter("FATHER_EMAIL", GetDbValue(dto.data.FatherEmail)),
    new OracleParameter("MOTHER_NAME", GetDbValue(dto.data.MotherName)),
    new OracleParameter("MOTHER_OCCUP", GetDbValue(dto.data.MotherOccupation)),
    new OracleParameter("MOTHER_MOBILE", GetDbValue(dto.data.MotherMobile)),
    new OracleParameter("MOTHER_EMAIL", GetDbValue(dto.data.MotherEmail)),
    new OracleParameter("GUARD_OCCUP", GetDbValue(dto.data.GuardianOccupation)),
    new OracleParameter("GUARD_MOBILE", GetDbValue(dto.data.GuardianMobile)),
    new OracleParameter("TOT_FUND", TryInt(dto.data.TotalFunding)),
    new OracleParameter("MON_FUND", TryInt(dto.data.MonthlyFunding)),
    new OracleParameter("GUARD_NAME", GetDbValue(dto.data.GuardianName)),
    new OracleParameter("GUARD_EMAIL", GetDbValue(dto.data.GuardianEmail)),
    new OracleParameter("COUNTRY", GetDbValue(dto.data.Country)),
    new OracleParameter("PROVINCE", GetDbValue(dto.data.Province)),
    new OracleParameter("LEADST", GetDbValue(dto.data.LeadStatus)),
    new OracleParameter("DOCNAME", GetDbValue(dto.data.DocumentName)),
    new OracleParameter("TRANSFERDATE", TryDate(dto.data.TransferDate)),
    new OracleParameter("TRANSFERSTATUS", GetDbValue(dto.data.TransferStatus)),
    new OracleParameter("REMARKS", GetDbValue(dto.data.Remark))
            };


            await _appDbContext.Database.ExecuteSqlRawAsync(sql, parameters);

            return Ok(new { status = "sucess", received = dto });
        }

        //Api For Website Data
        [HttpPost("/Website")]
        public async Task<IActionResult> PostLeads1([FromBody] StudentsDto dto)
        {



            if (dto == null)
            {
                return BadRequest("No Lead Available");
            }
            var Branch = "";

            if (dto.BranchName == "KANDY")
            {
                Branch = "ANC - KANDY BRANCH CAMPUS";
            }
            else if (dto.BranchName == "COLOMBO")
            {
                Branch = "COLOMBO";
            }


            var model = StudentMapper.ToModel(dto);



            var sql = @"
INSERT INTO ANCNEW.CHAT_LEADS(
     DOCID, DOCDATE, FNAME, LNAME, CONTACT, EMAIL, LEADSOURCE, SLEADSOURCE,
    LEADTYPE, SLEADTYPE, COUNSELLOR, PRE_PROLEVEL, BNAME, SCHOOL, TELEPHONE, DOB,
    SALUTATION, MNAME, P_TYPE, ADDR1, ADDR2, ADDR3, DISTRICT, GENDER, OL_YEAR,
    OL_TYPE, AL_YEAR, AL_TYPE, PREF_DESTINATION, PREF_FIELD, PREF_FIELD_OTHER,
    DEGREE, PROF_QUAL, PROF_QUAL_OTHER, OTHER_QUAL, EMP_COMPANY, DESIGNATION,
    WORK_EXP, SUBLEAD_OTHER, FATHER_NAME, FATHER_OCCUP, FATHER_MOBILE, FATHER_EMAIL,
    MOTHER_NAME, MOTHER_OCCUP, MOTHER_MOBILE, MOTHER_EMAIL, GUARD_OCCUP,
    GUARD_MOBILE, TOT_FUND, MON_FUND, GUARD_NAME, GUARD_EMAIL, COUNTRY, PROVINCE,
    LEADST, DOCNAME, TRANSFERDATE, TRANSFERSTATUS,REMARKS
)
VALUES(
     :DOCID, :DOCDATE, :FNAME, :LNAME, :CONTACT, :EMAIL, :LEADSOURCE, :SLEADSOURCE,
    :LEADTYPE, :SLEADTYPE, :COUNSELLOR, :PRE_PROLEVEL, :BNAME, :SCHOOL, :TELEPHONE, :DOB,
    :SALUTATION, :MNAME, :P_TYPE, :ADDR1, :ADDR2, :ADDR3, :DISTRICT, :GENDER, :OL_YEAR,
    :OL_TYPE, :AL_YEAR, :AL_TYPE, :PREF_DESTINATION, :PREF_FIELD, :PREF_FIELD_OTHER,
    :DEGREE, :PROF_QUAL, :PROF_QUAL_OTHER, :OTHER_QUAL, :EMP_COMPANY, :DESIGNATION,
    :WORK_EXP, :SUBLEAD_OTHER, :FATHER_NAME, :FATHER_OCCUP, :FATHER_MOBILE, :FATHER_EMAIL,
    :MOTHER_NAME, :MOTHER_OCCUP, :MOTHER_MOBILE, :MOTHER_EMAIL, :GUARD_OCCUP,
    :GUARD_MOBILE, :TOT_FUND, :MON_FUND, :GUARD_NAME, :GUARD_EMAIL, :COUNTRY, :PROVINCE,
    :LEADST, :DOCNAME, :TRANSFERDATE, :TRANSFERSTATUS,:REMARKS
)";

            var parameters = new[]
             {

    new OracleParameter("DOCID", GetDbValue(dto.DocumentId)),
    new OracleParameter("DOCDATE", TryDate1(dto.DocumentDate)),
    new OracleParameter("FNAME", GetDbValue(dto.FirstName)),
    new OracleParameter("LNAME", GetDbValue(dto.LastName)),
    new OracleParameter("CONTACT", GetDbValue(dto.ContactNumber)),
    new OracleParameter("EMAIL", GetDbValue(dto.Email)),
    new OracleParameter("LEADSOURCE", GetDbValue("Digital Marketing")),
    new OracleParameter("SLEADSOURCE", GetDbValue("Website")),
    new OracleParameter("LEADTYPE", GetDbValue(dto.LeadType)),
    new OracleParameter("SLEADTYPE", GetDbValue(dto.SubLeadType)),
    new OracleParameter("COUNSELLOR", GetDbValue(dto.Counsellor)),
    new OracleParameter("PRE_PROLEVEL", GetDbValue1(dto.PreviousProgramLevel)),
    new OracleParameter("BNAME", GetDbValue(Branch)),
    new OracleParameter("SCHOOL", GetDbValue(dto.SchoolName)),
    new OracleParameter("TELEPHONE", GetDbValue(dto.Telephone)),
    new OracleParameter("DOB", TryDate(dto.DateOfBirth)),
    new OracleParameter("SALUTATION", GetDbValue(dto.Salutation)),
    new OracleParameter("MNAME", GetDbValue(dto.MiddleName)),
    new OracleParameter("P_TYPE", GetDbValue1(dto.ProgramType)),
    new OracleParameter("ADDR1", GetDbValue(dto.AddressLine1)),
    new OracleParameter("ADDR2", GetDbValue(dto.AddressLine2)),
    new OracleParameter("ADDR3", GetDbValue(dto.AddressLine3)),
    new OracleParameter("DISTRICT", GetDbValue(dto.District)),
    new OracleParameter("GENDER", GetDbValue(dto.Gender)),
    new OracleParameter("OL_YEAR", GetDbValue(dto.OLevelYear)),
    new OracleParameter("OL_TYPE", GetDbValue(dto.OLevelType)),
    new OracleParameter("AL_YEAR", GetDbValue(dto.ALevelYear)),
    new OracleParameter("AL_TYPE", GetDbValue(dto.ALevelType)),
    new OracleParameter("PREF_DESTINATION", GetDbValue1(dto.PreferredDestination)),
    new OracleParameter("PREF_FIELD", GetDbValue(dto.PreferredField)),
    new OracleParameter("PREF_FIELD_OTHER", GetDbValue(dto.PreferredFieldOther)),
    new OracleParameter("DEGREE", GetDbValue(dto.Degree)),
    new OracleParameter("PROF_QUAL", GetDbValue(dto.ProfessionalQualification)),
    new OracleParameter("PROF_QUAL_OTHER", GetDbValue(dto.ProfessionalQualificationOther)),
    new OracleParameter("OTHER_QUAL", GetDbValue(dto.OtherQualification)),
    new OracleParameter("EMP_COMPANY", GetDbValue(dto.EmploymentCompany)),
    new OracleParameter("DESIGNATION", GetDbValue(dto.Designation)),
    new OracleParameter("WORK_EXP", TryInt(dto.WorkExperience)),
    new OracleParameter("SUBLEAD_OTHER", GetDbValue(dto.SubLeadOther)),
    new OracleParameter("FATHER_NAME", GetDbValue(dto.FatherName)),
    new OracleParameter("FATHER_OCCUP", GetDbValue(dto.FatherOccupation)),
    new OracleParameter("FATHER_MOBILE", GetDbValue(dto.FatherMobile)),
    new OracleParameter("FATHER_EMAIL", GetDbValue(dto.FatherEmail)),
    new OracleParameter("MOTHER_NAME", GetDbValue(dto.MotherName)),
    new OracleParameter("MOTHER_OCCUP", GetDbValue(dto.MotherOccupation)),
    new OracleParameter("MOTHER_MOBILE", GetDbValue(dto.MotherMobile)),
    new OracleParameter("MOTHER_EMAIL", GetDbValue(dto.MotherEmail)),
    new OracleParameter("GUARD_OCCUP", GetDbValue(dto.GuardianOccupation)),
    new OracleParameter("GUARD_MOBILE", GetDbValue(dto.GuardianMobile)),
    new OracleParameter("TOT_FUND", TryInt(dto.TotalFunding)),
    new OracleParameter("MON_FUND", TryInt(dto.MonthlyFunding)),
    new OracleParameter("GUARD_NAME", GetDbValue(dto.GuardianName)),
    new OracleParameter("GUARD_EMAIL", GetDbValue(dto.GuardianEmail)),
    new OracleParameter("COUNTRY", GetDbValue(dto.Country)),
    new OracleParameter("PROVINCE", GetDbValue(dto.Province)),
    new OracleParameter("LEADST", GetDbValue(dto.LeadStatus)),
    new OracleParameter("DOCNAME", GetDbValue(dto.DocumentName)),
    new OracleParameter("TRANSFERDATE", TryDate(dto.TransferDate)),
    new OracleParameter("TRANSFERSTATUS", GetDbValue(dto.TransferStatus)),
    new OracleParameter("REMARKS", GetDbValue(dto.Remark))
            };


            await _appDbContext.Database.ExecuteSqlRawAsync(sql, parameters);

            return Ok(new { status = "sucess", received = dto });
        }
        // Helpers 
        private static object GetDbValue(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? DBNull.Value : value.Trim();
        }

        private static object GetDbValue1(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? "NA" : value.Trim();
        }

        private static int? TryInt(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            return int.TryParse(value.Trim(), out var result) ? result : null;
        }

        private static decimal? TryDecimal(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            return decimal.TryParse(value.Trim(), out var result) ? result : null;
        }

        private static DateTime? TryDate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            return DateTime.TryParse(value.Trim(), out var result) ? result : null;
        }
        private static DateTime? TryDate1(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return DateTime.Now; ;
            return DateTime.TryParse(value.Trim(), out var result) ? result : DateTime.Now; ;
        }
    }
}
