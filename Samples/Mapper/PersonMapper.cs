using SerialLabs.Data.AzureTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
   public class PersonMapper : Mapper<Person>
    {
        public override DynamicEntity DomainToDto(Person domain)
        {
            DynamicEntity dto = base.DomainToDto(domain);
            dto.Set("Date", domain.Date);
            dto.Set("FirstName", domain.FirstName);
            dto.Set("LastName", domain.LastName);
            dto.Set("Number", domain.Number);
            dto.Set("Adr", domain.Adr);
            return dto;
        }
        public override Person DtoToDomain(DynamicEntity dto)
        {
            Person person= base.DtoToDomain(dto);
            
            person.FirstName = dto.Get<string>("FirstName");             
            person.Date = dto.Get<DateTime>("Date");            
            person.Number = dto.Get<int>("Number");
            person.Adr = dto.Get<Address>("Adr");
            return person;
        }
    }
}
