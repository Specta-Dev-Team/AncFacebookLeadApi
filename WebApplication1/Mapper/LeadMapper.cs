using WebApplication1.Model;
using System.Globalization;
namespace WebApplication1.Mapper
{
    public class LeadMapper
    {
        public static Lead ToModel(LeadDto dto) {

            return new Lead
            {
                LEADGENID = dto.leadgenId,
                FORMID = dto.formId,
                DATECREATED = dto.dateCreated,
                UNIVERSITYNAME=dto.data.university_name,
                LOCATION=dto.data.location,
                FIRSTNAME=dto.data.first_name,
                SECONDNAME=dto.data.second_name,
                EMAIL=dto.data.email,
                FULLNAME=dto.data.full_name,
                PHONENUMBER=dto.data.phone_number,


            };


        }

    }
}
