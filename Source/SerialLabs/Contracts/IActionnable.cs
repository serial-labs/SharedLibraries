using System.Threading.Tasks;

namespace SerialLabs
{
    /// <summary>
    /// IActionnable interface
    /// Reperesent an action that can be executed or canceled after execution
    /// </summary>
    public interface IActionnable
    {
        bool Execute(object targetData);

        Task<bool> ExecuteAsync(object targetData);

        bool Rollback(object targetData);

        Task<bool> RollbackAsync(object targetData);

    }
}