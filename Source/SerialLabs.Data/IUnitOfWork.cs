﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.Data
{
    /// <summary>
    /// A contract for a Unit of Work implementation.
    /// </summary>
    /// <see cref="http://martinfowler.com/eaaCatalog/unitOfWork.html"/>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves the pending changes
        /// </summary>
        void Save();
    }
}
