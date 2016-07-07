using System.Collections.Generic;

namespace Applicita.DataAccess
{
    /// <summary>
    /// Repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();
    }
}
