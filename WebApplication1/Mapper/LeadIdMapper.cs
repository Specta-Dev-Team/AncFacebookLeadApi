
using WebApplication1.Dto;
using WebApplication1.Model;

namespace WebApplication1.Mapper
{
    public class LeadIdMapper
    {
        public static Leadsid ToModel(Value dto)
        {
            return new Leadsid
            {
                LEADGENID = dto.leadgen_id

               

            };     
        }

    }
}
