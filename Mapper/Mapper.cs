using SerialLabs.Data.AzureTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
   
   public class Mapper<T, V> : IMapper<T, V> where V : struct where T : IEntity<V> ,new()
    {
        public DynamicEntity DomainToDto(T domain)
        {
            DynamicEntity dto = new DynamicEntity();
            dto.Add("Id", domain.Id);
            return dto;
        }

        public T DtoToDomain(DynamicEntity dto)
        {
            T domain= new T();
            V v = new V();
            dto.Get("Id",out v);
            domain.Id = v;
            return domain;
        }
    }
   
    public class Mapper<T> : IMapper<T> where T : IEntity<string>,new()
    {

        public virtual DynamicEntity DomainToDto(T domain)
        {
            DynamicEntity dto = new DynamicEntity();
            dto.Add("Id", domain.Id);
            return dto;
        }

        public virtual T DtoToDomain(DynamicEntity dto)
        {
            T domain = new T();
            string v = "";
            dto.Get("Id", out v);
            domain.Id = v;
            return domain;
        }
    }

   
  

}
