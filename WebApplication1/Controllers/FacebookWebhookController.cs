using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using System.Numerics;
using System.Text.Json;
using WebApplication1.Data;
using WebApplication1.Dto;
using WebApplication1.Mapper;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class FacebookWebhookController : Controller
    {

        public AppDbContext _appDbContext;


        public FacebookWebhookController(AppDbContext appDbContext)
        {

            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult VarifyKey([FromQuery (Name="hub.mode")] string hub_mode, [FromQuery(Name = "hub.challenge")] int hub_challenge, [FromQuery (Name = "hub.verify_token")] string hub_verify_token)
        {
            if (hub_mode == "subscribe" && hub_verify_token == "123987hytd@h72866hh")
            {
                return Ok(hub_challenge);
                    
            }

            return Ok("unortarized");
        }




        [HttpPost]
        public async Task<IActionResult> AddWebhook([FromBody]  LeadWebhookPayload dto)
        {

            Console.WriteLine("leadgenModels");

            if (dto == null)
            {
                return BadRequest("No Lead Available");
               
            }

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(dto, Newtonsoft.Json.Formatting.Indented));


            foreach (var entry in dto.Entry)
            {
                Console.WriteLine($"Entry ID: {entry.id}");
                foreach (var change in entry.changes)
                {
                    Console.WriteLine($"Change Field: {change.field}");
                    Console.WriteLine(JsonConvert.SerializeObject(change.value, Formatting.Indented));
                }
            }

            var leadgenModels = dto.Entry
            .SelectMany(e => e.changes)
            .Where(c => c.field == "leadgen")
            .Select(c => LeadIdMapper.ToModel(c.value))
            .ToList();

            Console.WriteLine("Hello"+Newtonsoft.Json.JsonConvert.SerializeObject(leadgenModels, Newtonsoft.Json.Formatting.Indented));

            foreach (var leads in leadgenModels) {

                long lead = leads.LEADGENID;

                using (var httpClient = new HttpClient()) {

                    string Url = $"https://graph.facebook.com/v18.0/{lead}?access_token=EAA4M2Gwv8rIBPBI3yl9JDC1fNaWD1vOYamvZBhIZAa5lZALMh11VJEs9ti5fdFTfE6ard6dCI9ERVZCtrsPGZBFWsdBnfTW23w0n3ZAQY2prafNdRYeXZB83TdTKyrSL6dBkEh2O6KgQJs7L167cmzfZAPxujJDwhTH4zGojmpPZBgbRkaGSl09ymTa8b70A4F5r79go11mbHMaEItCIDLTJg44JRZB7pnRhZCfMFdWVg1xZAEsb";
                    var response=await httpClient.GetAsync(Url);

                    if (response == null)
                    {
                        BadRequest("No Details entry");
                    }

                    string content =await response.Content.ReadAsStringAsync();


                    Console.WriteLine("This is Happen"+content);
                    var LeadData= JObject.Parse(content);
                    var FieldData = LeadData["field_data"];
                    string email = FieldData?.FirstOrDefault(f => f["name"]?.ToString() == "email")?["values"]?[0]?.ToString();
                    string name = FieldData?.FirstOrDefault(f => f["name"]?.ToString() == "full_name")?["values"]?[0]?.ToString();
                    string phoneNumber = FieldData?.FirstOrDefault(f => f["name"]?.ToString() == "phone")?["values"]?[0]?.ToString();


                    var sql = @"
                INSERT INTO ANCNEW.LEADS1 (leadgenId, email ,fullName, phoneNumber)
                VALUES (:leadgenId, :email,:fullName, :phoneNumber)
            ";

                    var parameters = new[]
                    {
                     new OracleParameter("LEADGENID", lead),
                     new OracleParameter("EMAIL", email ?? (object)DBNull.Value),
                     new OracleParameter("FULLNAME", name ?? (object)DBNull.Value),
                     new OracleParameter("PHONENUMBER", phoneNumber ?? (object)DBNull.Value)
            };

                    await _appDbContext.Database.ExecuteSqlRawAsync(sql, parameters);
                } ;

            }


            return Ok(new { status = "sucess" });

        }
    }
}
