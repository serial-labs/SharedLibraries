using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Data
{
    /// <summary>
    /// A contract for the Unit of Work implementation
    /// </summary>
    /// <see cref="http://martinfowler.com/eaaCatalog/unitOfWork.html"/>
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        /// <summary>
        /// Saves the pending changes (asynchronously)
        /// </summary>
        /// <returns></returns>
        Task<int> SaveAsync();
    }
}
