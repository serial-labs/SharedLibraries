using SerialLabs.Data.AzureTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public interface IMapper<T, V> where V : struct where T : IEntity<V>
    {
        DynamicEntity DomainToDto(T domain);
        T DtoToDomain(DynamicEntity dto);
    }

    public interface IMapper<T> where T : IEntity<string>
    {
        DynamicEntity DomainToDto(T domain);
        T DtoToDomain(DynamicEntity dto);
    }
}
