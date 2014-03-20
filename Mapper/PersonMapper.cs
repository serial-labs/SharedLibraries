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
            dto.Add("Date", domain.Date);
            dto.Add("FirstName", domain.FirstName);
            dto.Add("LastName", domain.LastName);
            dto.Add("Number", domain.Number);
            dto.Add("Adr", domain.Adr);
            return dto;
        }
        public override Person DtoToDomain(DynamicEntity dto)
        {
            Person person= base.DtoToDomain(dto);
            string firstname ;
            dto.Get("FirstName", out firstname);
            person.FirstName=firstname;
            DateTime date=new DateTime();
            dto.Get("Date", out date);
            person.Date = date;
            int number;
            dto.Get("Number",out number);
            person.Number = number;
            Address adr=new Address();
            dto.Get("Adr",out adr);
            person.Adr = adr;
            return person;
        }
    }
}
